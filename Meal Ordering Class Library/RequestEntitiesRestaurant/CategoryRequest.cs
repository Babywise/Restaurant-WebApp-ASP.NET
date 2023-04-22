using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.RequestEntitiesRestaurant
{
    public class CategoryRequest
    {
        public Category? Category { get; set; }
        public int? CategoryIdToDeleted { get; set; }
    }
}
