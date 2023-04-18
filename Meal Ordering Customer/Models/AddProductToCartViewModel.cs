using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Customer.Models
{
    public class AddProductToCartViewModel
    {
        public Product? Product { get; set; }
        public int? QuantityToAdd { get; set; }
    }
}
