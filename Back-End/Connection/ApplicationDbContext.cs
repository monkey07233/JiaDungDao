using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace JiaDungDao.Connection {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}