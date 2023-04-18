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

            if (getMenuRequest == null)
            {
                ModelState.AddModelError(string.Empty, $"Failed to get Menu from API.");

                return View();
            }

            ManagementViewModel managementViewModel = new ManagementViewModel()
            {
                Categories = getMenuRequest.Categories
            };

            return View(managementViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(ManagementViewModel model)
        {

            //GetOrdersRequest getOrdersRequest = await _managementService.GetOrdersAsync(HttpContext.Session.GetString("Authorization"));
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            if (ModelState.IsValid)
            {

                if (model.AddCategoryRequest != null)
                {
                    var response = await _managementService.AddCategoryAsync(model.AddCategoryRequest, HttpContext.Session.GetString("Authorization"));

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Management");
                    }
                }

                if (model.AddProductRequest != null)
                {
                    var response = await _managementService.AddProductAsync(model.AddProductRequest, HttpContext.Session.GetString("Authorization"));

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Management");
                    }
                }

                ModelState.AddModelError(string.Empty, $"Add Category \"{model.AddCategoryRequest.Name}\" failed");
            }

            return View(model);
        }

        [HttpGet("Management/Product/Add/")]
        public async Task<IActionResult> AddProductAsync()
        {
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));

            AddProductViewModel addProductViewModel = new AddProductViewModel()
            {
                Categories = getMenuRequest.Categories,
                AddProductRequest = new AddProductRequest()
            };

            return View("AddProduct", addProductViewModel);
        }

        [HttpPost("Management/Product/Add/")]
        public async Task<IActionResult> AddProductAsync(AddProductViewModel model)
        {
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            if (ModelState.IsValid)
            {
                var response = await _managementService.AddProductAsync(model.AddProductRequest, HttpContext.Session.GetString("Authorization"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Management");
                }

                ModelState.AddModelError(string.Empty, $"Add Product \"{model.AddProductRequest.Name}\" failed");
            }
            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
            model.Categories = getMenuRequest.Categories;
            /*
            ManagementViewModel managementViewModel = new ManagementViewModel()
            {
                Categories = getMenuRequest.Categories,
                AddCategoryRequest = new AddCategoryRequest(),
                AddProductRequest = new AddProductRequest()
            };

            return View("Index", managementViewModel);*/
            return View(model);
        }

    }
}
