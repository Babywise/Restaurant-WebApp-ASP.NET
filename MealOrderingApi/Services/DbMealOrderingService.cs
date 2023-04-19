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

        public async Task<Order> GetOrderByIdAsync(int OrderId)
        {
            return await _mealOrderingContext.Orders
                 .Where(o => o.OrderId == OrderId)
                 .Include(o => o.OrderProducts)
                 .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Order>> GetOrdersByUsernameAsync(string Username)
        {
            return await _mealOrderingContext.Orders
                .Where(o => o.Username == Username)
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

        // For status updates (checkout cart on customer side and switch status on restaurant side)
        public async Task<bool> UpdateOrderStatusAsync(UpdateOrderRequest updateOrderRequest)
        {
            Order orderFromDb = _mealOrderingContext.Orders.Where(o => o.OrderId == updateOrderRequest.Order.OrderId).FirstOrDefault();

            if (orderFromDb != null)
            {
                orderFromDb.Status = updateOrderRequest.Order.Status;
                _mealOrderingContext.Orders.Update(orderFromDb);
                if (await _mealOrderingContext.SaveChangesAsync() != 0) 
                { 
                    return true;
                }
            }
            return false;
        }

        // For updating the order products for the cart order
        public async Task<bool> UpdateOrderProductsAsync(UpdateOrderRequest updateOrderRequest)
        {
            // If the cart does not exist, create a new order with cart status for the given username and add the order products.
            // Else, the cart does exist so update as normal
            if (updateOrderRequest.Order.OrderId == 0 && updateOrderRequest.Order.Status == "Cart")
            {
                Order order = new Order
                {
                    OrderProducts = updateOrderRequest.Order.OrderProducts,
                    Username = updateOrderRequest.Order.Username,
                    Status = updateOrderRequest.Order.Status,
                    StoreId = updateOrderRequest.Order.StoreId
                };

                foreach (var o in order.OrderProducts)
                {
                    o.OrderProductId = 0;
                }
                _mealOrderingContext.Orders.Add(order);
                _mealOrderingContext.OrderProducts.AddRange(order.OrderProducts);
                if (await _mealOrderingContext.SaveChangesAsync() != 0)
                {
                    return true;
                }
            }
            else 
            { 
            
                ICollection<OrderProduct> orderProducts = _mealOrderingContext.OrderProducts.Where(o => o.OrderId == updateOrderRequest.Order.OrderId).ToList();

                if (orderProducts.Any())
                {
                
                    _mealOrderingContext.OrderProducts.Where(o => o.OrderId == updateOrderRequest.Order.OrderId).ExecuteDelete();

                    foreach (var o in updateOrderRequest.Order.OrderProducts) {
                        o.OrderProductId = 0;
                    }

                    _mealOrderingContext.OrderProducts.AddRange(updateOrderRequest.Order.OrderProducts);
                    if (await _mealOrderingContext.SaveChangesAsync() != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
