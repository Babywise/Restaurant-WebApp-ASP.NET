using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.Services;
using MealOrderingApi.DataAccess;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ICollection<Category>> GetMenuAsync()
        {
            return await _mealOrderingContext.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryAsync([FromQuery]int CategoryId)
        {
            return await _mealOrderingContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == CategoryId);
        }


        public async Task<ICollection<Order>> GetOrdersAsync()
        {
            return await _mealOrderingContext.Orders
                .Include(o => o.OrderProducts)
                .ToListAsync();
        }

        public async Task<bool> AddCategoryAsync(string categoryName)
        {
            if(!_mealOrderingContext.Categories.Where(c => c.Name == categoryName).Any())
            {
                _mealOrderingContext.Categories.Add(new Category()
                {
                    Name = categoryName
                });

                if (await _mealOrderingContext.SaveChangesAsync() != 0) 
                    return true;
            }
            return false;
        }
    }
}
