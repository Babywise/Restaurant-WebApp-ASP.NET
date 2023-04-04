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
        DbSet<Orderr> order { get; set; }
        DbSet<Accountt> account { get; set; }
        DbSet<Categoryy> category { get; set; }
        DbSet<Productt> product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
 