using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Repositories
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private readonly ApplicationDbContext db;
        public RestaurantRepo(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public List<Menu> GetAllMenuById(int Id)
        {
            var allMenu = db.Menu.Where(m => m.RestaurantID == Id).OrderBy(m => m.m_type).ToList();
            return allMenu;
        }

        public List<string> GetAllMenuTypeById(int Id)
        {
            var allMenuType = db.Menu.Where(m => m.RestaurantID == Id).OrderBy(m => m.m_type).Select(m => m.m_type).Distinct().ToList();
            return allMenuType;
        }

        public List<Restaurant> GetAllRestaurant()
        {
            return db.Restaurant.ToList();
        }

        public Restaurant GetRestaurantById(int Id)
        {
            return db.Restaurant.Where(r => r.RestaurantID == Id).FirstOrDefault();
        }
        public string updateRestaurant(Restaurant oldData, Restaurant restaurant)
        {
            var result = string.Empty;
            try
            {
                oldData.r_name = restaurant.r_name;
                oldData.r_address = restaurant.r_address;
                oldData.r_tel = restaurant.r_tel;
                var Count = db.SaveChanges();
                return (Count > 0) ? "success" : "fail";
            }
            catch (Exception e)
            {
                return e.ToString();
                throw;
            }
        }

        public string createRestaurant(Restaurant restaurant)
        {
            var result = string.Empty;
            try
            {
                db.Restaurant.Add(restaurant);
                db.SaveChanges();
                result = "successed";
            }
            catch (Exception e)
            {
                result = e.Message.ToString();
            }
            return result;
        }
        public int AddMenuItem(Menu newMenuItem)
        {
            try
            {
                db.Menu.Add(newMenuItem);
                return db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Debug.WriteLine(e);
                return -1;
            }
        }

        public bool DeleteMenu(int MenuID)
        {
            try
            {
                var Data = db.Menu.Where(m => m.MenuID == MenuID).FirstOrDefault();
                db.Menu.Remove(Data);
                var successCount = db.SaveChanges();
                if (successCount == 1)
                {
                    return true;
                }
                return false;
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public bool DeleteRestaurant(int RestaurantID)
        {
            try
            {
                var Data = db.Restaurant.Where(m => m.RestaurantID == RestaurantID).FirstOrDefault();
                db.Restaurant.Remove(Data);
                var successCount = db.SaveChanges();
                if (successCount == 1)
                {
                    var RestaurantsMenuData = db.Menu.Where(m => m.RestaurantID == RestaurantID).ToList();
                    db.Menu.RemoveRange(RestaurantsMenuData);
                    var menuSuccessCount = db.SaveChanges();
                    if (menuSuccessCount >= 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public string updateMenu(Menu menu)
        {
            var oldMenu = db.Menu.Where(m => m.MenuID == menu.MenuID).FirstOrDefault();
            try
            {
                oldMenu.m_item = menu.m_item;
                oldMenu.m_price = menu.m_price;
                oldMenu.m_type = menu.m_type;
                var count = db.SaveChanges();
                return (count > 0) ? "successed" : "fail";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public int GetLatestMenuId()
        {
            var menu = db.Menu.OrderByDescending(m => m.MenuID).FirstOrDefault();
            return menu.MenuID;
        }

        public string uploadMenuImg(int MenuID, string imgUrl)
        {
            var menu = db.Menu.Where(m => m.MenuID == MenuID).FirstOrDefault();
            try
            {
                menu.m_imgUrl = imgUrl;
                db.SaveChanges();
                return "Url上傳至DB成功";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string uploadRestaurantImg(int RestaurantID, string imgUrl)
        {
            var restaurant = db.Restaurant.Where(m => m.RestaurantID == RestaurantID).FirstOrDefault();
            try
            {
                restaurant.r_imgUrl = imgUrl;
                db.SaveChanges();
                return "Url上傳至DB成功";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
    }
}