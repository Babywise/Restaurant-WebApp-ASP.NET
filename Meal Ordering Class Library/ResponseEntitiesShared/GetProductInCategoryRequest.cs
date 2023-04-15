using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.ResponseEntitiesShared
{
    public class GetProductInCategoryRequest
    {
        public ICollection<Product>? Products { get; set; }
    }
}
