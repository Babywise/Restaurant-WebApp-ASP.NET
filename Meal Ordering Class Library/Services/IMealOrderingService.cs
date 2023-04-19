
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;

namespace Meal_Ordering_Class_Library.Services
{
    public interface IMealOrderingService
    {
        public Task<ICollection<Category>> GetMenuAsync();
        public Task<ICollection<Product>> GetProductsAsync();
        public Task<ICollection<Order>> GetOrdersAsync();
        public Task<bool> AddCategoryAsync(string categoryName);
        public Task<bool> AddProductAsync(Product product);
        public Task<bool> EditProductAsync(Product product);
        public Task<bool> DeleteProductAsync(int id);
    }
}
