using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;

namespace Meal_Ordering_Customer.Models
{
    public class OrderViewModel
    {
        // Holds the order
        public Order Order { get; set; }
        // Holds the full list of restaurant products.
        public ICollection<Category> Menu { get; set; }

    }
}
