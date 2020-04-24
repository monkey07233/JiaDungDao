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

        public List<Order> GetOrderInfo(string m_account)
        {
            return null;
            // var result = db.Order.Where(o => o.m_account == m_account).OrderByDescending(o => o.OrderID).ToList();
            // return result;
        }
    }
}