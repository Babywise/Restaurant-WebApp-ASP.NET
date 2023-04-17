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
        [HttpGet]
        public async Task<IActionResult> Menu()
        {
            return View();

        }
    } 
}
