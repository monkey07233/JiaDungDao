using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Repositories {
    public class RestaurantRepo : IRestaurantRepo {
        private readonly ApplicationDbContext db;
        public RestaurantRepo (ApplicationDbContext dbContext) 
        {
            this.db = dbContext;
        }

        public List<Menu> GetAllMenuById (int Id) 
        {
            var allMenu = db.Menu.Where (m => m.RestaurantID == Id).OrderBy (m => m.m_type).ToList ();
            return allMenu;
        }

        public List<string> GetAllMenuTypeById (int Id) 
        {
            var allMenuType = db.Menu.Where (m => m.RestaurantID == Id).OrderBy (m => m.m_type).Select (m => m.m_type).Distinct ().ToList ();
            return allMenuType;
        }

        public List<Restaurant> GetAllRestaurant () 
        {
            return db.Restaurant.ToList ();
        }

        public Restaurant GetRestaurantById (int Id) 
        {
            return db.Restaurant.Where (r => r.RestaurantID == Id).FirstOrDefault ();
        }
        public string updateRestaurant (Restaurant oldData,Restaurant restaurant) {
            var result = string.Empty;
            try {
                oldData.r_name = restaurant.r_name;
                oldData.r_address = restaurant.r_address;
                oldData.r_tel = restaurant.r_tel;
                var Count = db.SaveChanges ();
                return (Count > 0) ? "success" : "fail";
            } catch (Exception e) {
                return e.ToString ();
                throw;
            }
        }

        public string createRestaurant(Restaurant restaurant){
            var result = string.Empty;
            try{
                db.Restaurant.Add(restaurant);
                db.SaveChanges ();
                result = "successed";
            }catch(Exception e){
                result = e.Message.ToString();
            }
            return result;
        }
        public int AddMenuItem(Menu newMenuItem)
        {
            try{
                db.Menu.Add(newMenuItem);
                return db.SaveChanges();    
            }
            catch(DbUpdateException e){
                Debug.WriteLine(e);
                return -1;
            }         
        }
    }
}