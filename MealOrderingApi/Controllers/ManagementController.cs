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
    [Route("api/v1/[controller]")]
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

        [HttpGet("orders")]
        [Authorize(Roles = "Customer")]
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

        [HttpPost("add-category")]
        [Authorize]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest addCategoryRequest)
        {
            if (!await _mealOrderingService.AddCategoryAsync(addCategoryRequest.Name))
            {
                return BadRequest(new { Message = $"Category '{addCategoryRequest.Name}' not found" });
            }

            return Ok(new { Message = $"Category '{addCategoryRequest.Name}' was successfully added" });

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

    }
}
