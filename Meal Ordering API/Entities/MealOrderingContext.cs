using Microsoft.EntityFrameworkCore;

namespace Meal_Ordering_WebApp.Entities
{
    public class MealOrderingContext : DbContext
    {
        public MealOrderingContext(DbContextOptions<MealOrderingContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
 