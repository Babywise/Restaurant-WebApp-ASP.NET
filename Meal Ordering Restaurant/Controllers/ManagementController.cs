using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Restaurant.Models;
using Meal_Ordering_Restaurant.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Meal_Ordering_Restaurant.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ManagementService _managementService;
        public ManagementController(ManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
            GetOrdersRequest getOrdersRequest = await _managementService.GetOrdersAsync(HttpContext.Session.GetString("Authorization"));

            if (getMenuRequest == null)
            {
                ModelState.AddModelError(string.Empty, $"Failed to get Menu from API.");

                return View();
            }

            ManagementViewModel managementViewModel = new ManagementViewModel
            {
                Categories = getMenuRequest.Categories
            };

            return View(managementViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategories(ManagementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _managementService.AddCategoryAsync(model.AddCategoryRequest, HttpContext.Session.GetString("Authorization"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Management");
                }

                ModelState.AddModelError(string.Empty, $"Add Category \"{model.AddCategoryRequest.Name}\" failed");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(ManagementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _managementService.AddCategoryAsync(model.AddCategoryRequest, HttpContext.Session.GetString("Authorization"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Management");
                }

                ModelState.AddModelError(string.Empty, $"Add Category \"{model.AddCategoryRequest.Name}\" failed");
            }

            return View(model);
        }

    }
}
