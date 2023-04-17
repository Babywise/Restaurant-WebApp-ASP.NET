using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;

namespace Meal_Ordering_Restaurant.Models
{
    public class AddProductViewModel
    {
        public ICollection<Category>? Categories { get; set; }
        public AddProductRequest? AddProductRequest { get; set; }
    }
}
