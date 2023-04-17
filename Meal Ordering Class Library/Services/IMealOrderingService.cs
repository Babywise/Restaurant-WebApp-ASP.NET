
using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.Services
{
    public interface IMealOrderingService
    {
        public Task<ICollection<Category>> GetMenu();
    }
}
