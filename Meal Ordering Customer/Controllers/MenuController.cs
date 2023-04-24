using Meal_Ordering_Customer.Models;
using Meal_Ordering_Customer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.Services;

namespace Meal_Ordering_Customer.Controllers
{
    public class MenuController : Controller
    {
        private readonly OrderService _orderService;
        private readonly MenuService _menuService;
        private readonly IConfiguration _config;
        private readonly BaseJwtService _baseJwtService;
        public MenuController(IConfiguration config, OrderService orderService, MenuService menuService, BaseJwtService baseJwtService)
        {
            _config = config;
            _orderService = orderService;
            _menuService = menuService;
            _baseJwtService = baseJwtService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (!await _baseJwtService.CheckCustomerRoleClaimFromToken(accessToken))
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated

                GetMenuRequest getMenuRequest = await _menuService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));

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
        public async Task<IActionResult> DisplayItem(int ProductId, int CategoryId)
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (!await _baseJwtService.CheckCustomerRoleClaimFromToken(accessToken))
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated

                Product product = await _menuService.GetProductByIdAsync(HttpContext.Session.GetString("Authorization"), ProductId);
                bool IncludeProduct = false;
                product.Category = await _menuService.GetCategoryByIdAsync(HttpContext.Session.GetString("Authorization"), CategoryId, IncludeProduct);

                AddProductToCartViewModel vm = new AddProductToCartViewModel()
                {
                    Product = product,
                    QuantityToAdd = 0
                };

                return View(vm);
            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }

        }

    }
}

