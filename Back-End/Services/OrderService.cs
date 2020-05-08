using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;

namespace Back_End.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        public OrderService(IOrderRepo orderRepo)
        {
            this._orderRepo = orderRepo;
        }

        public List<OrderInfo> GetOrderInfo(string m_account)
        {
            return _orderRepo.GetOrderInfo(m_account);
        }

        public async Task<bool> CreateOrder(OrderInfo orderInfo)
        {
            return await _orderRepo.CreateOrder(orderInfo);
        }

        public List<OrderInfo> GetOrderInfoByResId(int restaurantId){
            return _orderRepo.GetOrderInfoByResId(restaurantId);
        }
    }
}