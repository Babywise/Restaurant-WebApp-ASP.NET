using Meal_Ordering_Class_Library.Entities;
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
                    ModelState.AddModelError(string.Empty, $"Add Category \"{model.AddCategoryRequest.Name}\" failed");
                }

                if (model.ProductRequest != null)
                {
                    var response = await _managementService.AddProductAsync(model.ProductRequest, HttpContext.Session.GetString("Authorization"));

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Management");
                    }
                }

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

            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductRequest = new ProductRequest(),
                //AddProductRequest = new AddProductRequest(),
                Categories = getMenuRequest.Categories
            };

            return View("AddProduct", productViewModel);
        }

        [HttpPost("Management/Product/Add/")]
        public async Task<IActionResult> AddProductAsync(ProductViewModel model)
        {
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            if (ModelState.IsValid)
            {
                var response = await _managementService.AddProductAsync(model.ProductRequest, HttpContext.Session.GetString("Authorization"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Management");
                }

                ModelState.AddModelError(string.Empty, $"Add Product \"{model.ProductRequest.Product.Name}\" failed. ({response.StatusCode}) : {response.RequestMessage}");
            }

            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
            model.Categories = getMenuRequest.Categories;

            return View(model);
        }


        [HttpGet("Management/Product/Edit/{id}")]
        public async Task<IActionResult> EditProductAsync(int id)
        {
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));

            ProductViewModel productViewModel = new ProductViewModel() 
            {
                EditProductRequest = new EditProductRequest()
                {
                    Product = (Product)getMenuRequest.Categories.First().Products.Where(p => p.ProductId == id),
                },
                Categories = getMenuRequest.Categories
            };

            return View("EditProduct", productViewModel);
        }

    }
}
