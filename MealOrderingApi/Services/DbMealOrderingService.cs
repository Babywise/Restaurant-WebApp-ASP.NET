using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
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

        public async Task<bool> AddCategoryAsync(string categoryName)
        {
            if (!_mealOrderingContext.Categories.Where(c => c.Name == categoryName && c.IsDeleted == false).Any())
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

        public async Task<bool> EditCategoryAsync(Category category)
        {
            Category categoryFromDb = _mealOrderingContext.Categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();

            if (categoryFromDb != null)
            {
                categoryFromDb.Name = category.Name;

                _mealOrderingContext.Categories.Update(categoryFromDb);
                if (await _mealOrderingContext.SaveChangesAsync() != 0)
                    return true;
            }
            return false;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            Category categoryFromDb = _mealOrderingContext.Categories.Where(c => c.CategoryId == id).FirstOrDefault();

            if (categoryFromDb != null)
            {
                categoryFromDb.IsDeleted = true;

                _mealOrderingContext.Categories.Update(categoryFromDb);
                if (await _mealOrderingContext.SaveChangesAsync() != 0)
                    return true;
            }
            return false;
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _mealOrderingContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync([FromQuery] int ProductId)
        {
            Product product = await _mealOrderingContext.Products
               .Include(p => p.Category)
               .FirstOrDefaultAsync(p => p.ProductId == ProductId);

            return product;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            if (!_mealOrderingContext.Products.Where(c => c.Name == product.Name && c.IsDeleted == false).Any())
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

            if (productFromDb != null)
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

            if (productFromDb != null)
            {
                productFromDb.IsDeleted = true;

                _mealOrderingContext.Products.Update(productFromDb);
                if (await _mealOrderingContext.SaveChangesAsync() != 0)
                    return true;
            }
            return false;
        }

        public async Task<ICollection<Order>> GetOrdersAsync()
        {
            return await _mealOrderingContext.Orders
                .Include(o => o.OrderProducts)
                .ToListAsync();
        }

        public async Task<ICollection<Order>> GetOrdersByUsernameAsync(string Username)
        {
            return await _mealOrderingContext.Orders
                .Where(o => o.Username == Username)
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
