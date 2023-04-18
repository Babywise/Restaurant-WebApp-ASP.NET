using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Customer.Models;
using Meal_Ordering_Customer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meal_Ordering_Customer.Controllers
{
    public class OrderController : Controller
    {
        private readonly CustomerService _customerService;
        public OrderController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // HTTP GET for the Quick Add, HTTP Post for the form submission of the Menu/DisplayItem Page
        public async Task<IActionResult> AddToCart(int CategoryId, int ProductId, int QuantityToAdd)
        {

            // Update the cart
            // ...

            // Redirect back to the Menu Display page
            return RedirectToAction("Display", "Menu", new { CategoryId = CategoryId });
        }

        // This is the view cart page
        [HttpGet("/Order/Cart")]
        public async Task<IActionResult> Cart()
        {
            throw new NotImplementedException();
        }

        // This is the order history page
        [HttpGet("/Order/List")]
        public async Task<IActionResult> List()
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                string Username = HttpContext.Session.GetString("Username");
                GetOrdersRequest orders = await _customerService.GetOrdersByUsernameAsync(HttpContext.Session.GetString("Authorization"), Username);

                return View(orders);
            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }
        }

        // This is the order details page
        [HttpGet("/Order/View/{OrderId}")]
        public async Task<IActionResult> View(int OrderId)
        {

            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
                }

                Order order = await _customerService.GetOrderAsync(HttpContext.Session.GetString("Authorization"), OrderId);
                GetMenuRequest gmr = await _customerService.GetMenuAsync(HttpContext.Session.GetString("Authorization"));

                OrderViewModel ovm = new OrderViewModel {
                    Order = order,
                    Menu = gmr.Categories
                };

                return View(ovm);
            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if not authenticated
            }


        }

        [HttpPost("/Order/Create")]
        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

    } 
}
