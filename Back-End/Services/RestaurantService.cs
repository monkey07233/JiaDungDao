using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Back_End.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepo RestaurantRepo;
        public static IWebHostEnvironment _environment;

        public RestaurantService(IRestaurantRepo restaurantRepo, IWebHostEnvironment environment)
        {
            this.RestaurantRepo = restaurantRepo;
            _environment = environment;
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
            var result = RestaurantRepo.createRestaurant(restaurant);
            return result;
        }

        public int AddMenuItem(Menu newMenuItem){
            return RestaurantRepo.AddMenuItem(newMenuItem);
        }

        public bool DeleteMenu(int MenuID)
        {
            var isSuccess = RestaurantRepo.DeleteMenu(MenuID);
            return isSuccess;
        }

        public bool DeleteRestaurant(int RestaurantID)
        {
            var isSuccess = RestaurantRepo.DeleteRestaurant(RestaurantID);
            return isSuccess;
        }

        public async Task<string> uploadRestaurantImg(Restaurant restaurant, IFormFile files)
        {
            try {
                if (files != null) {
                    if (!Directory.Exists (_environment.ContentRootPath + "\\File\\Restaurant\\")) {
                        Directory.CreateDirectory (_environment.ContentRootPath + "\\File\\Restaurant\\");
                    }
                    using (FileStream stream = System.IO.File.Create (_environment.ContentRootPath + "\\File\\Restaurant\\" + restaurant.r_name + "_餐廳封面_" + files.FileName)) {
                        await files.CopyToAsync (stream);
                        stream.Flush ();
                    }
                    return "新增成功且照片上傳完成";
                }
                return "新增成功但照片上傳失敗";
            } catch (System.Exception e) {
                return e.Message.ToString ();
            }
        }
    }
}