using Microsoft.AspNetCore.Mvc;

namespace Meal_Ordering_Restaurant.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
