﻿using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.Services;
using Microsoft.AspNetCore.Authorization;
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
            var categories = await _mealOrderingService.GetAllMenuItemsAsync();

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

    }
}
