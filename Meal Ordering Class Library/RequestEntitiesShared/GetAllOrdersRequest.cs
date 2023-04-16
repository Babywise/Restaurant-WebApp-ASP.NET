using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.RequestEntitiesShared
{
    public class GetAllOrdersRequest
    {
        ICollection<Order>? Orders { get; set; }
    }
}
