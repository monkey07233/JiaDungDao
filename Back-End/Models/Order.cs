using System;
using System.ComponentModel.DataAnnotations;
namespace Back_End.Models {
    public class Order {
        [Key]
        public int OrderDetailId {get;set;}
        public int OrderId { get; set; }
        public string o_item { get; set; }
        public int o_count { get; set; }
        public int o_price { get; set; }
    }
}