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

        [HttpGet("product")]
        [Authorize]
        public async Task<IActionResult> Product([FromQuery] int ProductId)
        {
            var product = await _mealOrderingService.GetProductAsync(ProductId);

            if (product == null)
            {
                return NotFound(new { Message = "Category not found" });
            }

            return Ok(product);

        }

        [HttpGet("orders")]
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

    }
}
