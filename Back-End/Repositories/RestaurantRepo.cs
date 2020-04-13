using System.Collections.Generic;
using System.Linq;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;

namespace Back_End.Repositories
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private readonly ApplicationDbContext db;
        public RestaurantRepo(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }
        public List<Restaurant> GetAllRestaurant()
        {
            var result = db.Restaurant.ToList();
            if (result != null)
            {
                return result;
            }else{
                return null;
            }
        }
    }
}