using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.RequestEntitiesShared
{
    public class GetOrdersRequest
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
