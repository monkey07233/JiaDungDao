using Microsoft.AspNetCore.Http;
namespace Back_End.Models {
    public class RestaurantInfo {
        public int RestaurantID { get; set; }
        public string r_name { get; set; }
        public int MenuID { get; set; }
        public string m_item { get; set; }
        public int uploadType { get; set; }
        public IFormFile files { get; set; }
    }
}