using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.RequestEntitiesShared
{
    //Use for update CART and CHECKOUT (Customer) (order status)
    //Use for update ORDER (Restaurant)
    public class UpdateOrderRequest
    {
        public Order? Order { get; set; }
    }
}
