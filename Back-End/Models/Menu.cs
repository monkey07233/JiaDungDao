using System.ComponentModel.DataAnnotations;
namespace Back_End.Models {
    public class Menu {
        [Key]
        public int MenuID { get; set; }
        public int RestaurantID { get; set; }
        public string m_item { get; set; }
        public string m_type { get; set; }
        public int m_price { get; set; }
    }
}