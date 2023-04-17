
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;

namespace Meal_Ordering_Class_Library.Services
{
    public interface IMealOrderingService
    {
        public Task<ICollection<Category>> GetMenuAsync();
        public Task<Category> GetCategoryAsync(int categoryId);
        public Task<ICollection<Order>> GetOrdersAsync();
        public Task<bool> AddCategoryAsync(string categoryName);
    }
}
