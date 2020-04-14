using System.Collections.Generic;
using Back_End.Interface;
using Back_End.Models;

namespace Back_End.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepo RestaurantRepo;
        public RestaurantService(IRestaurantRepo restaurantRepo)
        {
            this.RestaurantRepo = restaurantRepo;
        }
        public List<Restaurant> GetAllRestaurant()
        {
            var result = RestaurantRepo.GetAllRestaurant();
            if (result != null)
            {
                return result;
            }else{
                return null;
            }
        }
    }
}