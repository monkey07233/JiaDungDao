using System.Collections.Generic;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IRestaurantRepo
    {
        List<Restaurant> GetAllRestaurant();
    }
}