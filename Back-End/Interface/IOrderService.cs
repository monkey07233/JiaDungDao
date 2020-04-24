using System.Collections.Generic;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IOrderService
    {
        List<Order> GetOrderInfo(string m_account);
        string createOrder(List<Order> Orders);
    }
}