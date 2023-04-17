using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;

namespace Meal_Ordering_Restaurant.Models
{
    public class ManagementViewModel
    {
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Product>? Products { get; set; }
        public AddCategoryRequest? AddCategoryRequest { get; set; }
    }
}
