using System.Collections.Generic;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IOrderRepo
    {
        List<OrderInfo> GetOrderInfo(string m_account);
        string createOrder(OrderInfo orderInfo);
    }
}