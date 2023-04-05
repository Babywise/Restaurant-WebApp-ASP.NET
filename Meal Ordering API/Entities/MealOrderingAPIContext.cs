using Meal_Ordering_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meal_Ordering_WebApp.Entities
{
    public class MealOrderingAPIContext : DbContext
    {
        public MealOrderingAPIContext(DbContextOptions<MealOrderingAPIContext> options)
            : base(options)
        {
        }
         public DbSet<Order> order { get; set; }
        public DbSet<Account> account { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Product> product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
 