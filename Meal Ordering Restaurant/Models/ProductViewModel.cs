using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;

namespace Meal_Ordering_Restaurant.Models
{
    public class ProductViewModel
    {
        public ICollection<Category>? Categories { get; set; }
        public ProductRequest? ProductRequest { get; set; }
        public AddProductRequest? AddProductRequest { get; set; }
        public EditProductRequest? EditProductRequest { get; set; }
    }
}
