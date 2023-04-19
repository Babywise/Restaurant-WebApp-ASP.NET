using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
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

        public async Task<ICollection<Category>> GetMenuAsync()
        {
            return await _mealOrderingContext.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _mealOrderingContext.Products
                .Include(p => p.Category)
                .ToListAsync();
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

        public async Task<bool> AddProductAsync(Product product)
        {
            if(!_mealOrderingContext.Products.Where(c => (c.Name == product.Name) && c.IsDeleted == false).Any())
            {
                _mealOrderingContext.Products.Add(product);

                if (await _mealOrderingContext.SaveChangesAsync() != 0) 
                    return true;
            }
            return false;
        }

        public async Task<bool> EditProductAsync(Product product)
        {
            Product productFromDb = _mealOrderingContext.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();

            if(productFromDb != null)
            {
                productFromDb.Name = product.Name;
                productFromDb.Description = product.Description;
                productFromDb.Cost = product.Cost;
                productFromDb.Quantity = product.Quantity;
                productFromDb.CategoryId = product.CategoryId;

                _mealOrderingContext.Products.Update(productFromDb);
                if (await _mealOrderingContext.SaveChangesAsync() != 0)
                    return true;
            }
            return false;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            Product productFromDb = _mealOrderingContext.Products.Where(p => p.ProductId == id).FirstOrDefault();

            if(productFromDb != null)
            {
                productFromDb.IsDeleted = true;

                _mealOrderingContext.Products.Update(productFromDb);
                if (await _mealOrderingContext.SaveChangesAsync() != 0)
                    return true;
            }
            return false;
        }

    }
}
