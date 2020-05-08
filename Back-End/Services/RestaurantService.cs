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
        private readonly IRestaurantRepo _restaurantRepo;
        public static IWebHostEnvironment _environment;

        public RestaurantService (IRestaurantRepo restaurantRepo, IWebHostEnvironment environment) {
            this._restaurantRepo = restaurantRepo;
            _environment = environment;
        }
        public List<Restaurant> GetAllRestaurant () {
            return _restaurantRepo.GetAllRestaurant ();
        }

        public RestaurantAndMenu GetRestaurantInfoById (int id) {
            var restaurantResult = _restaurantRepo.GetRestaurantById (id);
            var menuTypeResult = _restaurantRepo.GetAllMenuTypeById (id);
            var menuResult = _restaurantRepo.GetAllMenuById (id);
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

        public bool UpdateRestaurant (Restaurant restaurant) {
            var oldData = _restaurantRepo.GetRestaurantById (restaurant.RestaurantID);
            if (oldData != null) {
                return _restaurantRepo.UpdateRestaurant (oldData, restaurant);
            }
            return false;
        }

        public int CreateRestaurant (Restaurant restaurant) {
            var result = _restaurantRepo.CreateRestaurant (restaurant);
            if (result) {
                return _restaurantRepo.GetLatestRestaurantId ();
            }
            return 0;
        }

        public bool AddMenuItem (Menu newMenuItem) {
            var menu = _restaurantRepo.GetMenuByName (newMenuItem.RestaurantID, newMenuItem.m_item);
            if (menu == null) {
                return _restaurantRepo.AddMenuItem (newMenuItem);
            }
            return false;
        }

        public bool DeleteMenu (int menuID) {
            return _restaurantRepo.DeleteMenu (menuID);
        }

        public async Task<bool> DeleteRestaurant (int restaurantID) {
            return await _restaurantRepo.DeleteRestaurant (restaurantID);
        }

        public bool UpdateMenu (Menu menu) {
            return _restaurantRepo.UpdateMenu (menu);
        }

        public int GetLatestMenuId () {
            return _restaurantRepo.GetLatestMenuId ();
        }

        public bool DeleteRestaurantAllImg (int restaurantID) {
            string restaurantPath = _environment.ContentRootPath + "\\File\\Restaurant\\";
            string menuPath = _environment.ContentRootPath + "\\File\\Menu\\";
            try {
                string[] restaurantImg = Directory.GetFiles (restaurantPath, restaurantID + ".jpg");
                if (restaurantImg != null) {
                    foreach (var rImg in restaurantImg) {
                        System.IO.File.Delete (rImg);
                    }
                    var allRestaurantMenu = _restaurantRepo.GetAllMenuById (restaurantID);
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

        public bool DeleteMenuImg (int menuID) {
            string path = _environment.ContentRootPath + "\\File\\Menu\\";
            try {
                string[] menuImg = Directory.GetFiles (path, menuID + ".jpg");
                if (menuImg != null) {
                    foreach (string img in menuImg) {
                        System.IO.File.Delete (img);
                    }
                }
                return true;
            } catch (System.Exception e) {
                return false;
            }
        }
    }
}