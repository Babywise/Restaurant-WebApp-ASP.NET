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

            return View();

        }
    } 
}
