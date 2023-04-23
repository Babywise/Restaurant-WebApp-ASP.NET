using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.RequestEntitiesRestaurant
{
    public class ProductRequest
    {
        public Product? Product { get; set; }
        public int? ProductIdToDeleted { get; set; }
    }
}
