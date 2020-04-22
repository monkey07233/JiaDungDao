using System.Collections.Generic;

namespace Back_End.Models {
    public class RestaurantAndMenu {
        public Restaurant restaurant { get; set; }
        public List<TypeAndMenu> typeAndMenu { get; set; }
    }
}