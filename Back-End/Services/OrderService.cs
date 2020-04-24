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

        public List<Order> GetOrderInfo(string m_account)
        {
            var result = OrderRepo.GetOrderInfo(m_account);
            return result;
        }
    }
}