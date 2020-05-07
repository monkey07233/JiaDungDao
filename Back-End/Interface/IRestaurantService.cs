using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IRestaurantService
    {
        List<Restaurant> GetAllRestaurant();
        RestaurantAndMenu GetRestaurantInfoById(int id);
        bool UpdateRestaurant(Restaurant restaurant);
        int CreateRestaurant(Restaurant restaurant);
        bool AddMenuItem(Menu newMenuItem);
        bool DeleteMenu(int menuID);
        Task<bool> DeleteRestaurant(int restaurantID);
        bool DeleteRestaurantAllImg(int restaurantID);
        bool UpdateMenu(Menu menu);
        int GetLatestMenuId();
        Task<string> UploadImg(UploadInfo uploadInfo);
        bool DeleteMenuImg(int menuID);
    }
}