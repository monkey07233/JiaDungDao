using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IRestaurantService
    {
        List<Restaurant> GetAllRestaurant();
        RestaurantAndMenu GetRestaurantInfoById(int Id);
        string updateRestaurant(Restaurant restaurant);
        int createRestaurant(Restaurant restaurant);
        int AddMenuItem(Menu newMenuItem);
        bool DeleteMenu(int MenuID);
        bool DeleteRestaurant(int RestaurantID);
        bool DeleteRestaurantAllImg(int RestaurantID);
        string updateMenu(Menu menu);
        int GetLatestMenuId();
        Task<string> UploadImg(UploadInfo uploadInfo);
        string DeleteMenuImg(int MenuID);
    }
}