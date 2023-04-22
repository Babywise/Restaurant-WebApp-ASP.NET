using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealOrderingApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController()]
    public class MenuController : Controller
    {
        private readonly IMealOrderingService _mealOrderingService;

        public MenuController(IMealOrderingService mealOrderingService)
        {
            _mealOrderingService = mealOrderingService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
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
            var product = await _mealOrderingService.GetProductByIdAsync(ProductId);

            if (product == null)
            {
                return NotFound(new { Message = "Category not found" });
            }
            return Ok(product);
        }

    }
}
