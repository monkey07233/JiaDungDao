using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Back_End.Services {
    public class RestaurantService : IRestaurantService {
        private readonly IRestaurantRepo RestaurantRepo;
        public static IWebHostEnvironment _environment;

        public RestaurantService (IRestaurantRepo restaurantRepo, IWebHostEnvironment environment) {
            this.RestaurantRepo = restaurantRepo;
            _environment = environment;
        }
        public List<Restaurant> GetAllRestaurant () {
            var result = RestaurantRepo.GetAllRestaurant ();
            if (result != null) {
                return result;
            } else {
                return null;
            }
        }

        public RestaurantAndMenu GetRestaurantInfoById (int Id) {
            var restaurantResult = RestaurantRepo.GetRestaurantById (Id);
            var menuTypeResult = RestaurantRepo.GetAllMenuTypeById (Id);
            var menuResult = RestaurantRepo.GetAllMenuById (Id);
            RestaurantAndMenu result = new RestaurantAndMenu ();
            result.restaurant = restaurantResult;
            List<TypeAndMenu> typeAndMenus = new List<TypeAndMenu> ();
            foreach (var type in menuTypeResult) {
                TypeAndMenu typeAndMenu = new TypeAndMenu ();
                typeAndMenu.m_type = type;
                List<Menu> restaurantMenu = new List<Menu> ();
                foreach (var item in menuResult) {
                    if (item.m_type == type) {
                        restaurantMenu.Add (item);
                    }
                }
                typeAndMenu.menu = restaurantMenu;
                typeAndMenus.Add (typeAndMenu);
            }
            result.typeAndMenu = typeAndMenus;
            return result;
        }

        public string updateRestaurant (Restaurant restaurant) {
            var result = string.Empty;
            var oldData = RestaurantRepo.GetRestaurantById (restaurant.RestaurantID);
            if (oldData != null) {
                result = RestaurantRepo.updateRestaurant (oldData, restaurant);
            }
            return result;
        }

        public int createRestaurant (Restaurant restaurant) {
            var result = RestaurantRepo.createRestaurant (restaurant);
            if (result == "successed") {
                return RestaurantRepo.GetLatestRestaurantId ();
            }
            return 0;
        }

        public int AddMenuItem (Menu newMenuItem) {
            return RestaurantRepo.AddMenuItem (newMenuItem);
        }

        public bool DeleteMenu (int MenuID) {
            var isSuccess = RestaurantRepo.DeleteMenu (MenuID);
            return isSuccess;
        }

        public bool DeleteRestaurant (int RestaurantID) {
            var isSuccess = RestaurantRepo.DeleteRestaurant (RestaurantID);
            return isSuccess;
        }

        public string updateMenu (Menu menu) {
            var result = RestaurantRepo.updateMenu (menu);
            return result;
        }

        public int GetLatestMenuId () {
            return RestaurantRepo.GetLatestMenuId ();
        }

        public bool DeleteRestaurantAllImg (int RestaurantID) {
            string restaurantPath = _environment.ContentRootPath + "\\File\\Restaurant\\";
            string menuPath = _environment.ContentRootPath + "\\File\\Menu\\";
            try {
                string[] restaurantImg = Directory.GetFiles (restaurantPath, RestaurantID + ".jpg");
                if (restaurantImg != null) {
                    foreach (var rImg in restaurantImg) {
                        System.IO.File.Delete (rImg);
                    }
                    var allRestaurantMenu = RestaurantRepo.GetAllMenuById (RestaurantID);
                    if (allRestaurantMenu != null && allRestaurantMenu.Count > 0) {
                        foreach (var menu in allRestaurantMenu) {
                            string[] menuImg = Directory.GetFiles (menuPath, menu.MenuID + ".jpg");
                            foreach (var mImg in menuImg) {
                                System.IO.File.Delete (mImg);
                            }
                        }
                    }
                }
                return true;
            } catch (System.Exception e) {
                return false;
            }
        }

        public async Task<string> UploadImg (UploadInfo uploadInfo) {
            try {
                string root = _environment.ContentRootPath;
                string rootFile = string.Empty;
                string imgUrl = string.Empty;
                if (uploadInfo.files != null) {
                    //uploadType 0:餐廳封面 ,  1:菜單圖片 , 2:使用者大頭貼 
                    switch (uploadInfo.uploadType) {
                        case 0:
                            root += "\\File\\Restaurant\\";
                            break;
                        case 1:
                            root += "\\File\\Menu\\";
                            break;
                        case 2:
                            root += "\\File\\UserImg\\";
                            break;
                    }
                    rootFile = root + uploadInfo.id + ".jpg";

                    if (!Directory.Exists (root)) {
                        Directory.CreateDirectory (root);
                    }
                    using (FileStream stream = System.IO.File.Create (rootFile)) {
                        await uploadInfo.files.CopyToAsync (stream);
                        stream.Flush ();
                    }
                    return "上傳成功";
                }
                return "上傳失敗";
            } catch (System.Exception e) {
                return e.Message.ToString ();
            }
        }
      
        public string DeleteMenuImg (int MenuID) {
            string path = _environment.ContentRootPath + "\\File\\Menu\\";
            try {
                string[] menuImg = Directory.GetFiles (path, MenuID + ".jpg");
                if (menuImg != null) {
                    foreach (string img in menuImg) {
                        System.IO.File.Delete (img);
                    }
                }
                return "刪除照片成功";
            } catch (System.Exception e) {
                return e.Message.ToString ();
            }
        }
    }
}