using Microsoft.AspNetCore.Mvc;

namespace Meal_Ordering_API.Controllers
{
    public class StoreManagementController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
