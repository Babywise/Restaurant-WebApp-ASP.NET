using Microsoft.AspNetCore.Mvc;

namespace Meal_Ordering_API.Controllers
{
    public class OrderingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
