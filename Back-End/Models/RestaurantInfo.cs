using Microsoft.AspNetCore.Http;
namespace Back_End.Models {
    public class RestaurantInfo {
        public int RestaurantID { get; set; }
        public string r_name { get; set; }
        public IFormFile files { get; set; }
    }
}