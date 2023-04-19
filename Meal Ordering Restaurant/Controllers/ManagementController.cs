using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Restaurant.Models;
using Meal_Ordering_Restaurant.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            // ----LOAD FROM SESSION
            var categoryIdFromSession = HttpContext.Session.GetInt32("SelectedCategoryId");
            if (categoryIdFromSession != null)
            {
                managementViewModel.SelectedCategoryId = categoryIdFromSession;
            }
            var categoryNameFromSession = HttpContext.Session.GetString("SelectedCategoryName");
            if (categoryNameFromSession != null)
            {
                managementViewModel.SelectedCategoryName = categoryNameFromSession;
            }
            var productNameFromSession = HttpContext.Session.GetString("SelectedProductName");
            if (productNameFromSession != null)
            {
                managementViewModel.SelectedProductName = productNameFromSession;
            }

            // ----END OF SESSION MGMT

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

            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
            model.Categories = getMenuRequest.Categories;

            if (model.SelectedCategoryName != null)
            {
                model.CategoryRequest = new CategoryRequest()
                {
                    Category = new Category
                    {
                        Name = model.SelectedCategoryName
                    }
                };
                var response = await _managementService.AddCategoryAsync(model.CategoryRequest, HttpContext.Session.GetString("Authorization"));
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    HttpContext.Session.Remove("SelectedCategoryName"); // CLEAR SESSION ON SUCCESS
                    return RedirectToAction("Index", "Management");
                }
                TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
            }

            if (model.ProductRequest != null)
            {
                if (!string.IsNullOrEmpty(model.ProductRequest.Product.Name) && !string.IsNullOrEmpty(model.ProductRequest.Product.Description) &&
                    model.ProductRequest.Product.Quantity != null && model.ProductRequest.Product.Cost != null &&
                    model.ProductRequest.Product.CategoryId != null)
                {
                    var response = await _managementService.AddProductAsync(model.ProductRequest, HttpContext.Session.GetString("Authorization"));
                    var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                        HttpContext.Session.Remove("SelectedProductName"); // CLEAR SESSION ON SUCCESS
                        return RedirectToAction("Index", "Management");
                    }
                    TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    //ModelState.AddModelError(string.Empty, $"Add Category \"{model.CategoryRequest.Category.Name}\" failed");
                }
                ProductViewModel productViewModel = new ProductViewModel
                {
                    ProductRequest = model.ProductRequest,
                    Categories = getMenuRequest.Categories,
                };
                return View("AddProduct", productViewModel);
            }

            if (model.SelectedProductName != null)
            {
                if (model.ProductRequest == null)
                {
                    model.ProductRequest = new ProductRequest()
                    {
                        Product = new Product
                        {
                            Name = model.SelectedProductName
                        }
                    };
                }

                ProductViewModel productViewModel = new ProductViewModel()
                {
                    ProductRequest = model.ProductRequest,
                    Categories = getMenuRequest.Categories,
                };

                return View("AddProduct", productViewModel);
                
            }

            if (model.SelectedProductName != null)
            {
                ProductViewModel productViewModel = new ProductViewModel()
                {
                    ProductRequest = new ProductRequest()
                    {
                        Product = new Product()
                        {
                            Name = model.SelectedProductName,
                        }
                    },
                    Categories = getMenuRequest.Categories,
                };
                return RedirectToAction("AddProduct", model.SelectedProductName);
                //return View("AddProduct", productViewModel);
            }

            /* else if (model.ProductRequest.Product.Name != null)
            {
                ProductViewModel productViewModel = new ProductViewModel()
                {
                    ProductRequest = model.ProductRequest,
                    Categories = getMenuRequest.Categories,
                };

                return View("AddProduct", productViewModel);
            }*/



            // ----START OF SESSION MGMT

            if (model.SelectedCategoryId != null)
            {
                HttpContext.Session.SetInt32("SelectedCategoryId", (int)model.SelectedCategoryId);
            }
            else
            {
                var categoryIdFromSession = HttpContext.Session.GetInt32("SelectedCategoryId");
                if (categoryIdFromSession != null)
                {
                    model.SelectedCategoryId = categoryIdFromSession;
                }
            }

            if (model.SelectedCategoryName != null)
            {
                HttpContext.Session.SetString("SelectedCategoryName", model.SelectedCategoryName);
            }
            else
            {
                var categoryNameFromSession = HttpContext.Session.GetString("SelectedCategoryName");
                if (categoryNameFromSession != null)
                {
                    model.SelectedCategoryName = categoryNameFromSession;
                }
            }

            if (model.SelectedProductName != null)
            {
                HttpContext.Session.SetString("SelectedProductName", model.SelectedProductName);
            }
            else
            {
                var productNameFromSession = HttpContext.Session.GetString("SelectedProductName");
                if (productNameFromSession != null)
                {
                    model.SelectedProductName = productNameFromSession;
                }
            }

            // ----END OF SESSION MGMT

            return View(model);
        }

        [HttpGet("Management/Product/Add/")]
        public async Task<IActionResult> AddProductAsync(string productName)
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

            if (ModelState.IsValid)
            {
                productViewModel.ProductRequest.Product.Name = productName;
            }

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
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    return RedirectToAction("Index", "Management");
                }
                TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
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
                ProductRequest = new ProductRequest()
                {
                    Product = getMenuRequest.Categories.Where(c => c.CategoryId == HttpContext.Session.GetInt32("SelectedCategoryId")).First().Products.Where(p => p.ProductId == id).First(),
                },
                Categories = getMenuRequest.Categories
            };

            return View("EditProduct", productViewModel);
        }

        [HttpPost("Management/Product/Edit/{id}")]
        public async Task<IActionResult> EditProductAsync(ProductViewModel model)
        {
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            if (ModelState.IsValid)
            {
                var response = await _managementService.EditProductAsync(model.ProductRequest, HttpContext.Session.GetString("Authorization"));
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    return RedirectToAction("Index", "Management");
                }
                TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
            }

            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
            model.Categories = getMenuRequest.Categories;

            return View(model);
        }

        [HttpPost("Management/Product/Delete/{id}")]
        public async Task<IActionResult> DeleteProductAsync(ProductViewModel model)
        {
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            if (model.ProductRequest.ProductIdToDeleted != null)
            {
                var response = await _managementService.DeleteProductAsync(model.ProductRequest, HttpContext.Session.GetString("Authorization"));
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    return RedirectToAction("Index", "Management");
                }
                TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
            }

            GetMenuRequest getMenuRequest = await _managementService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));
            model.Categories = getMenuRequest.Categories;

            return RedirectToAction("Index", "Management");
        }

    }
}
