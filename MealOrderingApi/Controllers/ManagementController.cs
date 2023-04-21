using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Class_Library.Services;
using MealOrderingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealOrderingApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController()]
    public class ManagementController : ControllerBase
    {
        private readonly IMealOrderingService _mealOrderingService;

        public ManagementController(IMealOrderingService mealOrderingService)
        {
            _mealOrderingService = mealOrderingService;
        }

        [HttpGet("menu")]
        [Authorize]
        public async Task<IActionResult> Menu()
        {
            var categories = await _mealOrderingService.GetMenuAsync();

            if (categories == null)
            {
                return NotFound(new { Message = "Menu not found" });
            }
            GetMenuRequest getAllCategoriesRequest = new GetMenuRequest()
            {
                Categories = categories
            };
            return Ok(getAllCategoriesRequest);
        }

        [HttpGet("category")]
        [Authorize]
        public async Task<IActionResult> Category([FromQuery] int CategoryId, [FromQuery] bool IncludeProduct)
        {
            var category = await _mealOrderingService.GetCategoryAsync(CategoryId, IncludeProduct);

            if (category == null)
            {
                return NotFound(new { Message = "Category not found" });
            }
            return Ok(category);
        }

        [HttpPost("add-category")]
        [Authorize]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest categoryRequest)
        {
            if (!await _mealOrderingService.AddCategoryAsync(categoryRequest.Category.Name))
            {
                return BadRequest(new { Message = $"Category '{categoryRequest.Category.Name}' could not be added" });
            }
            return Ok(new { Message = $"Category '{categoryRequest.Category.Name}' was successfully added" });
        }

        [HttpPut("edit-category")]
        [Authorize]
        public async Task<IActionResult> EditCategory([FromBody] CategoryRequest categoryRequest)
        {
            if (!await _mealOrderingService.EditCategoryAsync(categoryRequest.Category))
            {
                return BadRequest(new { Message = $"Category '{categoryRequest.Category.Name}' could not be edited" });
            }
            return Ok(new { Message = $"Category '{categoryRequest.Category.Name}' was successfully edited" });
        }

        [HttpPost("delete-category")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory([FromBody] CategoryRequest categoryRequest)
        {
            var categories = await _mealOrderingService.GetMenuAsync();
            var categoryName = categories.Where(c => c.CategoryId == categoryRequest.CategoryIdToDeleted).First().Name;
            if (!await _mealOrderingService.DeleteCategoryAsync((int)categoryRequest.CategoryIdToDeleted))
            {
                return BadRequest(new { Message = $"Category 'id = {categoryRequest.CategoryIdToDeleted}' could not be deleted" });
            }
            return Ok(new { Message = $"Category '{categoryName}' was successfully deleted" });
        }

        [HttpGet("product")]
        [Authorize]
        public async Task<IActionResult> Product([FromQuery] int ProductId)
        {
            var product = await _mealOrderingService.GetProductByIdAsync(ProductId);

            if (product == null)
            {
                return NotFound(new { Message = "Category not found" });
            }
            return Ok(product);
        }

        [HttpPost("add-product")]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest)
        {
            if (!await _mealOrderingService.AddProductAsync(productRequest.Product))
            {
                return BadRequest(new { Message = $"Product '{productRequest.Product.Name}' could not be added" });
            }
            return Ok(new { Message = $"Product '{productRequest.Product.Name}' was successfully added" });
        }

        [HttpPut("edit-product")]
        [Authorize]
        public async Task<IActionResult> EditProduct([FromBody] ProductRequest productRequest)
        {
            if (!await _mealOrderingService.EditProductAsync(productRequest.Product))
            {
                return BadRequest(new { Message = $"Product '{productRequest.Product.Name}' could not be edited" });
            }
            return Ok(new { Message = $"Product '{productRequest.Product.Name}' was successfully edited" });
        }

        [HttpPost("delete-product")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct([FromBody] ProductRequest productRequest)
        {
            if (!await _mealOrderingService.DeleteProductAsync((int)productRequest.ProductIdToDeleted))
            {
                return BadRequest(new { Message = $"Product 'id = {productRequest.ProductIdToDeleted}' could not be deleted" });
            }
            var products = await _mealOrderingService.GetProductsAsync();
            return Ok(new { Message = $"Product '{products.Where(p => p.ProductId == productRequest.ProductIdToDeleted).First().Name}' was successfully deleted" });
        }

        [HttpGet("all-orders")]
        [Authorize]
        public async Task<IActionResult> Orders()
        {
            var orders = await _mealOrderingService.GetOrdersAsync();

            if (orders == null)
            {
                return NotFound(new { Message = "Orders not found" });
            }
            GetOrdersRequest getOrdersRequest = new GetOrdersRequest()
            {
                Orders = orders
            };
            return Ok(getOrdersRequest);
        }

        [HttpGet("orders")]
        [Authorize]
        public async Task<IActionResult> OrdersByCustomerUsername(string Username)
        {
            var orders = await _mealOrderingService.GetOrdersByUsernameAsync(Username);

            if (orders == null)
            {
                return NotFound(new { Message = "Orders not found" });
            }
            GetOrdersRequest getOrdersRequest = new GetOrdersRequest()
            {
                Orders = orders
            };
            return Ok(getOrdersRequest);
        }

        [HttpPut("update-order")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest updateOrderRequest)
        {
            if (updateOrderRequest != null)
            {
                if (updateOrderRequest.Order != null)
                {
                    if (updateOrderRequest.Order.OrderId == 0 && updateOrderRequest.Order.Status == "Cart")
                    {
                        if (!await _mealOrderingService.UpdateOrderProductsAsync(updateOrderRequest))
                        {
                            return BadRequest(new { Message = $"Order '{updateOrderRequest.Order.OrderId}' failed to update" });
                        }
                        else
                        {
                            return Ok(new { Message = "Cart Order was successfully updated" });
                        }
                    }
                    else
                    {
                        // Always update order status
                        if (!await _mealOrderingService.UpdateOrderStatusAsync(updateOrderRequest))
                        {
                            return BadRequest(new { Message = $"Order '{updateOrderRequest.Order.OrderId}' status failed to update" });
                        }

                        // If the order products are not null (we are likely adding a product from cart), update order products
                        if (updateOrderRequest.Order.OrderProducts != null)
                        {
                            if (!await _mealOrderingService.UpdateOrderProductsAsync(updateOrderRequest))
                            {
                                return BadRequest(new { Message = $"Order '{updateOrderRequest.Order.OrderId}' failed to update" });
                            }
                        }
                        else
                        {
                            return Ok(new { Message = $"Order '{updateOrderRequest.Order.OrderId}' status was successfully updated" });
                        }
                        return Ok(new { Message = $"Order '{updateOrderRequest.Order.OrderId}' was successfully updated" });
                    }
                }
            }
            return NotFound(new { Message = "Order not found" });
        }

    }
}
