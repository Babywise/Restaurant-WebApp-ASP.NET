
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;

namespace Meal_Ordering_Class_Library.Services
{
    public interface IMealOrderingService
    {
        public Task<ICollection<Category>> GetMenuAsync();
        public Task<Category> GetCategoryAsync(int CategoryId, bool IncludeProduct);
        public Task<Product> GetProductAsync(int ProductId);
        public Task<ICollection<Order>> GetOrdersAsync();
        public Task<ICollection<Order>> GetOrdersByUsernameAsync(string Username);
        public Task<Order> GetOrderByIdAsync(int OrderId);
        public Task<bool> AddCategoryAsync(string categoryName);
    }
}
