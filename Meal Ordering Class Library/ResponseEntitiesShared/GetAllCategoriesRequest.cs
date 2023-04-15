using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.ResponseEntitiesShared
{
    public class GetAllCategoriesRequest
    {
        public ICollection<Category>? Categories { get; set; }
    }
}
