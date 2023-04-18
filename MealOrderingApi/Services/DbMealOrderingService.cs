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
                .Include(c => c.Products).Where(p => p.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryAsync([FromQuery] int CategoryId, bool IncludeProduct)
        {
            if (IncludeProduct)
            {
                return await _mealOrderingContext.Categories
                .Include(c => c.Products).Where(p => p.IsDeleted != true)
                .FirstOrDefaultAsync(c => c.CategoryId == CategoryId);
            }
            else 
            {
                return await _mealOrderingContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == CategoryId);
            }
        }

        public async Task<Product> GetProductAsync([FromQuery] int ProductId)
        {
            Product product = await _mealOrderingContext.Products
               .Include(p => p.Category)
               .FirstOrDefaultAsync(p => p.ProductId == ProductId);

            // For security reasons. We do not want people to get the product info for "soft-deleted" items.
            // So make the product null except for the description, which tells us that it has been deleted.
            if (product.IsDeleted == true) {
                product = null;
                product.Description = "Deleted Item, Cannot Fetch";
            }

            return product;

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
