using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.RequestEntitiesShared
{
    public class GetAllProductsRequest
    {
        public ICollection<Product>? Products { get; set; }
    }
}
