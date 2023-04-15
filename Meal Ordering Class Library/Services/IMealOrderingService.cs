
using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.Services
{
    public interface IMealOrderingService
    {
        public ICollection<Account> GetAccounts();
    }
}
