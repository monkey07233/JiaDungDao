using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IRestaurantRepo
    {
        List<Restaurant> GetAllRestaurant();
        Restaurant GetRestaurantById(int id);
        List<string> GetAllMenuTypeById(int id);
        List<Menu> GetAllMenuById(int id);
        bool UpdateRestaurant(Restaurant oldData,Restaurant restaurant);
        bool CreateRestaurant(Restaurant restaurant);
        bool AddMenuItem(Menu newMenuItem);
        bool DeleteMenu(int menuID);
        Task<bool> DeleteRestaurant(int restaurantID);
        bool UpdateMenu(Menu menu);
        int GetLatestMenuId ();
        int GetLatestRestaurantId();
        Menu GetMenuByName(int restaurantID,string m_item);
    }
}