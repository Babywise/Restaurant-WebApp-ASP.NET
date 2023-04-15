using Meal_Ordering_Class_Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meal_Ordering_API.Controllers
{
    [ApiController()]
    public class AccountController : Controller
    {
        private IMealOrderingService _mealOrderingService;

        public AccountController(IMealOrderingService mealOrderingService)
        {
            _mealOrderingService = mealOrderingService;
        }



    }
}
