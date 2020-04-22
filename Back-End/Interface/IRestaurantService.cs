using System.Collections.Generic;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IRestaurantService
    {
        List<Restaurant> GetAllRestaurant();
        RestaurantAndMenu GetRestaurantInfoById(int Id);
        string updateRestaurant(Restaurant restaurant);
        string createRestaurant(Restaurant restaurant);
        int AddMenuItem(Menu newMenuItem);
    }
}