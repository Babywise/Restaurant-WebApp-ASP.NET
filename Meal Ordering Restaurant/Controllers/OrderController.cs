using Microsoft.AspNetCore.Mvc;

namespace Meal_Ordering_Restaurant.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
