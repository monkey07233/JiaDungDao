using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;

namespace Back_End.Repositories {
    public class OrderRepo : IOrderRepo {
        private readonly ApplicationDbContext db;
        public OrderRepo (ApplicationDbContext dbContext) {
            this.db = dbContext;
        }

        public List<OrderInfo> GetOrderInfo (string m_account) {
            var orderTitleResult = db.OrderTitle.Where (o => o.m_account == m_account).OrderByDescending (o => o.OrderId).ToList ();
            return AssembleOrderInfo (orderTitleResult);
        }

        public List<OrderInfo> GetOrderInfoByResId (int restaurantId) {
            var orderTitleResult = db.OrderTitle.Where (o => o.RestaurantID == restaurantId).OrderByDescending (o => o.OrderId).ToList ();
            return AssembleOrderInfo (orderTitleResult);
        }

        public List<OrderInfo> AssembleOrderInfo (List<OrderTitle> orderTitleList) {
            List<OrderInfo> result = new List<OrderInfo> ();
            foreach (var title in orderTitleList) {
                OrderInfo tmp = new OrderInfo ();
                var orderResult = db.Order.Where (o => o.OrderId == title.OrderId).OrderBy (o => o.OrderDetailId).ToList ();
                tmp.title = title;
                tmp.orderDetail = orderResult;
                result.Add (tmp);
            }
            return result;
        }

        public async Task<bool> CreateOrder (OrderInfo orderInfo) {
            try {
                orderInfo.title.o_createtime = DateTime.Now;
                db.OrderTitle.Add (orderInfo.title);
                await db.SaveChangesAsync();
                var orderTitleResult = db.OrderTitle.Where (o => o.m_account == orderInfo.title.m_account).OrderByDescending (o => o.OrderId).FirstOrDefault ();
                foreach (var order in orderInfo.orderDetail) {
                    order.OrderId = orderTitleResult.OrderId;
                    db.Order.Add (order);
                }
                await db.SaveChangesAsync ();
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}