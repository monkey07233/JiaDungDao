using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IOrderService
    {
        List<OrderInfo> GetOrderInfo(string m_account);
        Task<bool> CreateOrder(OrderInfo orderInfo);
        List<OrderInfo> GetOrderInfoByResId(int restaurantId);
    }
}