using System;
using System.ComponentModel.DataAnnotations;
namespace Back_End.Models {
    public class Order {
        [Key]
        public int OrderID { get; set; }
        public string o_item { get; set; }
        public int o_count { get; set; }
        public int o_price { get; set; }
        public DateTime o_time { get; set; }
        public string m_account { get; set; }
    }
}