using System.Collections.Generic;
using System.Linq;
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

        public RestaurantAndMenu GetRestaurantInfoById(int Id)
        {
            var restaurantResult = RestaurantRepo.GetRestaurantById(Id);
            var menuTypeResult = RestaurantRepo.GetAllMenuTypeById(Id);
            var menuResult = RestaurantRepo.GetAllMenuById(Id);
            RestaurantAndMenu result = new RestaurantAndMenu();
            result.restaurant = restaurantResult;
            List<TypeAndMenu> typeAndMenus = new List<TypeAndMenu>();
            foreach (var type in menuTypeResult)
            {
                TypeAndMenu typeAndMenu = new TypeAndMenu();
                typeAndMenu.m_type = type;
                List<Menu> restaurantMenu = new List<Menu>();
                foreach (var item in menuResult)
                {
                    if (item.m_type == type)
                    {
                        restaurantMenu.Add(item);
                    }
                }
                typeAndMenu.menu = restaurantMenu;
                typeAndMenus.Add(typeAndMenu);
            }
            result.typeAndMenu = typeAndMenus;
            return result;
        }

        public string updateRestaurant(Restaurant restaurant)
        {
            var result = string.Empty;
            var oldData = RestaurantRepo.GetRestaurantById(restaurant.RestaurantID);
            if (oldData != null)
            {
                result = RestaurantRepo.updateRestaurant(oldData,restaurant);
            }
            return result;
        }

        public string createRestaurant(Restaurant restaurant){
            return RestaurantRepo.createRestaurant(restaurant);
        }

        public int AddMenuItem(Menu newMenuItem){
            return RestaurantRepo.AddMenuItem(newMenuItem);
        }
    }
}