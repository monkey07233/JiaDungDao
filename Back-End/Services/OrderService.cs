using System.Collections.Generic;
using Back_End.Interface;
using Back_End.Models;

namespace Back_End.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo OrderRepo;
        public OrderService(IOrderRepo orderRepo)
        {
            this.OrderRepo = orderRepo;
        }

        public List<OrderInfo> GetOrderInfo(string m_account)
        {
            return OrderRepo.GetOrderInfo(m_account);
        }

        public string createOrder(OrderInfo orderInfo)
        {
            return OrderRepo.createOrder(orderInfo);
        }

        public List<OrderInfo> GetOrderInfoByResId(int restaurantId){
            return OrderRepo.GetOrderInfoByResId(restaurantId);
        }
    }
}