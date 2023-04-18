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

        public async Task<IActionResult> AddToCart(int CategoryId, int ProductId, int QuantityToAdd)
        {
            // Add the specified quantity to the cart
            // ...

            // Redirect back to the Menu Display page
            return RedirectToAction("Display", "Menu", new { CategoryId = CategoryId });
        }

    } 
}
