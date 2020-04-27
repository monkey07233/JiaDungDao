using System;
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
                var orderResult = db.Order.Where(o => o.OrderId == title.OrderId).OrderBy(o => o.OrderDetailId).ToList();
                tmp.title = title;
                tmp.orderDetail = orderResult;
                result.Add(tmp);
            }
            return result;
        }


        public string createOrder(OrderInfo orderInfo)
        {
            var result = string.Empty;
            try
            {
                orderInfo.title.o_createtime = DateTime.Now;
                db.OrderTitle.Add(orderInfo.title);
                db.SaveChanges();
                var orderTitleResult = db.OrderTitle.Where(o => o.m_account == orderInfo.title.m_account).OrderByDescending(o => o.OrderId).FirstOrDefault();
                foreach (var order in orderInfo.orderDetail)
                {
                    order.OrderId = orderTitleResult.OrderId;
                    db.Order.Add(order);
                }
                db.SaveChanges();
                result = "successed";
            }
            catch (Exception e)
            {
                result = e.Message.ToString();
            }
            return result;
        }
    }
}