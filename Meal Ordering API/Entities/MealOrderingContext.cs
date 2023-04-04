using Meal_Ordering_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meal_Ordering_WebApp.Entities
{
    public class MealOrderingContext : DbContext
    {
        public MealOrderingContext(DbContextOptions<MealOrderingContext> options)
            : base(options)
        {
        }
        DbSet<Order> order { get; set; }
        DbSet<Account> account { get; set; }
        DbSet<Category> category { get; set; }
        DbSet<Product> product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
