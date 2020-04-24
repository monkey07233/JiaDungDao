using System;
using System.ComponentModel.DataAnnotations;
namespace Back_End.Models {
    public class OrderTitle {
        [Key]
        public int OrderId { get; set; }
        public int RestaurantID { get; set; }
        public string m_account { get; set; }
        public DateTime o_createtime { get; set; }
        public int o_total { get; set; }
    }
}