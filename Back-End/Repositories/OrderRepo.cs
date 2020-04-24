using System.Collections.Generic;
using System.Linq;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;

namespace Back_End.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext db;
        public OrderRepo(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public List<OrderInfo> GetOrderInfo(string m_account)
        {
            List<OrderInfo> result = new List<OrderInfo>();
            var orderTitleResult = db.OrderTitle.Where(o => o.m_account == m_account).OrderByDescending(o => o.OrderId).ToList();
            foreach (var title in orderTitleResult)
            {
                OrderInfo tmp = new OrderInfo();
                var orderResult = db.Order.Where(o => o.OrderID == title.OrderId).OrderBy(o => o.Id).ToList();
                tmp.title = title;
                tmp.orderDetail = orderResult;
                result.Add(tmp);
            }
            return result;
        }
    }
}