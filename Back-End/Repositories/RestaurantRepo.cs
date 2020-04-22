using System.Collections.Generic;
using System.Linq;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;

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
    }
}