using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealOrderingApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController()]
    public class OrderController : Controller
    {
        private readonly IMealOrderingService _mealOrderingService;

        public OrderController(IMealOrderingService mealOrderingService)
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
