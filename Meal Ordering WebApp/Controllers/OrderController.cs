using Microsoft.AspNetCore.Mvc;

namespace Meal_Ordering_WebApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
