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

        public List<Order> GetOrderInfo(string m_account)
        {
            var result = db.Order.Where(o => o.m_account == m_account).OrderByDescending(o => o.OrderID).ToList();
            return result;
        }


        public string createOrder(List<Order> Orders)
        {
            var result = string.Empty;
            try
            {
                foreach (var order in Orders)
                {
                    order.o_time = DateTime.Now;
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