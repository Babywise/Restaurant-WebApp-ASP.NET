using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.Services;
using MealOrderingApi.DataAccess;

namespace MealOrderingApi.Services
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
