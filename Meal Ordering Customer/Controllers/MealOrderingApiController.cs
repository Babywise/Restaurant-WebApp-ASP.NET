using Microsoft.AspNetCore.Mvc;

namespace Meal_Ordering_WebApp.Controllers
{
    [ApiController()]
    public class MealOrderingApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
