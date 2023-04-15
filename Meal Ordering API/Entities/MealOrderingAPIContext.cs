using Meal_Ordering_Class_Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meal_Ordering_WebApp.Entities
{
    public class MealOrderingAPIContext : DbContext
    {
        public MealOrderingAPIContext(DbContextOptions<MealOrderingAPIContext> options)
            : base(options)
        {
        }

         public DbSet<Order> Order { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
 