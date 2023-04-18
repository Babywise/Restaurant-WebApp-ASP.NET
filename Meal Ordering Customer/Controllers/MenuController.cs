using Meal_Ordering_Customer.Models;
using Meal_Ordering_Customer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Class_Library.Models;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.Entities;
using System.ComponentModel;

namespace Meal_Ordering_Customer.Controllers
{
    public class MenuController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly IConfiguration _config;
        public MenuController(IConfiguration config, CustomerService customerService)
        {
            _config = config;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                GetMenuRequest getMenuRequest = await _customerService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));

                if (getMenuRequest == null)
                {
                    ModelState.AddModelError(string.Empty, $"Failed to get Menu from API.");
                }

                return View(getMenuRequest.Categories);

            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

        }

        [HttpGet]
        public async Task<IActionResult> Display(int CategoryId) {
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            bool IncludeProduct = true;
            Category category = await _customerService.GetCategoryAsync(HttpContext.Session.GetString("Authorization"), CategoryId, IncludeProduct);

            return View(category);

        }

        [HttpGet]
        public async Task<IActionResult> DisplayItem(int ProductId, int CategoryId)
        {
            string accessToken = HttpContext.Session.GetString("Authorization");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

            Product product = await _customerService.GetProductAsync(HttpContext.Session.GetString("Authorization"), ProductId);
            bool IncludeProduct = false;
            product.Category = await _customerService.GetCategoryAsync(HttpContext.Session.GetString("Authorization"), CategoryId, IncludeProduct);

            AddProductToCartViewModel vm = new AddProductToCartViewModel()
            {
                Product = product,
                QuantityToAdd = 0
            };

            return View(vm);

        }

    }
}

