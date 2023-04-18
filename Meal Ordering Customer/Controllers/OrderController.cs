using Meal_Ordering_Class_Library.RequestEntitiesShared;
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
            // Add the specified quantity to the cart
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
            throw new NotImplementedException();
        }

        // This is the order details page
        [HttpGet("/Order/View/{OrderId}")]
        public async Task<IActionResult> View(int OrderId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("/Order/Create")]
        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

    } 
}
