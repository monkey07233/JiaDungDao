using System.Collections.Generic;

namespace Back_End.Models {
    public class OrderInfo {
        public OrderTitle title { get; set; }
        public List<Order> orderDetail { get; set; }
    }
}