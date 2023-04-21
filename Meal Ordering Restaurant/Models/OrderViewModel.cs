using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;

namespace Meal_Ordering_Restaurant.Models
{
    public class OrderViewModel
    {
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public UpdateOrderRequest? UpdateOrderRequest { get; set; }
        public string? SelectedStatus { get; set; }
        public string? SelectedTab { get; set; } = "active";
    }
}
