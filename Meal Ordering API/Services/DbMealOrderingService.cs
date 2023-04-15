using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.Services;
using Meal_Ordering_WebApp.Entities;

namespace Meal_Ordering_API.Services
{
    public class DbMealOrderingService : IMealOrderingService
    {
        private MealOrderingAPIContext _mealOrderingContext;
        public DbMealOrderingService(MealOrderingAPIContext mealOrderingContext) 
        {
            _mealOrderingContext = mealOrderingContext;
        }
    }
}
