using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace JiaDungDao.Connection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Restaurant> restaurant { get; set; }
    }
}