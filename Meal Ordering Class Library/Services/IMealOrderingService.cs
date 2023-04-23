using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;

namespace Meal_Ordering_Class_Library.Services
{
    public interface IMealOrderingService
    {
        //Management
        public Task<ICollection<Category>> GetAllMenuItemsAsync();
        public Task<bool> AddCategoryAsync(string categoryName);
        public Task<bool> EditCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int id);
        public Task<Product> GetProductByIdAsync(int ProductId);
        public Task<bool> AddProductAsync(Product product);
        public Task<bool> EditProductAsync(Product product);
        public Task<bool> DeleteProductAsync(int id);
        
        //Menu
        public Task<ICollection<Category>> GetMenuAsync();
        public Task<ICollection<Product>> GetProductsAsync();
        public Task<Category> GetCategoryAsync(int CategoryId, bool IncludeProduct);

        //Order
        public Task<ICollection<Order>> GetOrdersAsync();
        public Task<ICollection<Order>> GetOrdersByUsernameAsync(string Username);
        public Task<bool> UpdateOrderStatusAsync(UpdateOrderRequest updateOrderRequest);
        public Task<bool> UpdateOrderProductsAsync(UpdateOrderRequest updateOrderRequest);
    }
}
