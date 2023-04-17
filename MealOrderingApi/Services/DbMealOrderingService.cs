using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.Services;
using MealOrderingApi.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MealOrderingApi.Services
{
    public class DbMealOrderingService : IMealOrderingService
    {
        private MealOrderingAPIContext _mealOrderingContext;
        public DbMealOrderingService(MealOrderingAPIContext mealOrderingContext) 
        {
            _mealOrderingContext = mealOrderingContext;
        }

        public async Task<ICollection<Category>> GetMenu()
        {
            return await _mealOrderingContext.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }
        public async Task<ICollection<Order>> GetOrders()
        {
            return await _mealOrderingContext.Orders
                .Include(o => o.OrderProducts)
                .ToListAsync();
        }
    }
}
