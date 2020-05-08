using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Repositories {
    public class RestaurantRepo : IRestaurantRepo {
        private readonly ApplicationDbContext db;
        public RestaurantRepo (ApplicationDbContext dbContext) {
            this.db = dbContext;
        }

        public List<Menu> GetAllMenuById (int id) {
            return db.Menu.Where (m => m.RestaurantID == id).OrderBy (m => m.m_type).ToList ();
        }

        public List<string> GetAllMenuTypeById (int id) {
            return db.Menu.Where (m => m.RestaurantID == id).OrderBy (m => m.m_type).Select (m => m.m_type).Distinct ().ToList ();
        }

        public List<Restaurant> GetAllRestaurant () {
            return (from m in db.Member from r in db.Restaurant where (m.m_account == r.m_account && m.isBlock != true) select r).ToList ();
        }

        public Restaurant GetRestaurantById (int id) {
            return db.Restaurant.Where (r => r.RestaurantID == id).FirstOrDefault ();
        }
        public bool UpdateRestaurant (Restaurant oldData, Restaurant restaurant) {
            try {
                oldData.r_name = restaurant.r_name;
                oldData.r_address = restaurant.r_address;
                oldData.r_tel = restaurant.r_tel;
                db.SaveChanges ();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public bool CreateRestaurant (Restaurant restaurant) {
            try {
                db.Restaurant.Add (restaurant);
                db.SaveChanges ();
                return true;
            } catch (Exception e) {
                return false;
            }
        }
        public bool AddMenuItem (Menu newMenuItem) {
            try {
                db.Menu.Add (newMenuItem);
                db.SaveChanges ();
                return true;
            } catch (DbUpdateException e) {
                return false;
            }
        }

        public bool DeleteMenu (int menuID) {
            try {
                var data = db.Menu.Where (m => m.MenuID == menuID).FirstOrDefault ();
                db.Menu.Remove (data);
                db.SaveChanges ();
                return true;
            } catch (System.Exception e) {
                return false;
            }
        }

        public async Task<bool> DeleteRestaurant (int restaurantID) {
            try {
                var data = db.Restaurant.Where (m => m.RestaurantID == restaurantID).FirstOrDefault ();
                db.Restaurant.Remove (data);
                await db.SaveChangesAsync ();
                
                var restaurantsMenuData = db.Menu.Where (m => m.RestaurantID == restaurantID).ToList ();
                db.Menu.RemoveRange (restaurantsMenuData);
                await db.SaveChangesAsync ();
                return true;
            } catch (System.Exception e) {
                return false;
            }
        }

        public bool UpdateMenu (Menu menu) {
            var oldMenu = db.Menu.Where (m => m.MenuID == menu.MenuID).FirstOrDefault ();
            try {
                oldMenu.m_item = menu.m_item;
                oldMenu.m_price = menu.m_price;
                oldMenu.m_type = menu.m_type;
                db.SaveChanges ();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public int GetLatestMenuId () {
            var menu = db.Menu.OrderByDescending (m => m.MenuID).FirstOrDefault ();
            return menu.MenuID;
        }
        public int GetLatestRestaurantId () {
            var restaurant = db.Restaurant.OrderByDescending (r => r.RestaurantID).FirstOrDefault ();
            return restaurant.RestaurantID;
        }

        public Menu GetMenuByName (int restaurantID, string m_item) {
            return db.Menu.Where (m => m.RestaurantID == restaurantID && m.m_item == m_item).FirstOrDefault ();
        }
    }
}