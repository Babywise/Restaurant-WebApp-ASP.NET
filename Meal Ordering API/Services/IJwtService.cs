using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_API.Services
{
    public interface IJwtService
    {
        public Task<string> GenerateJwtToken(User user);
    }
}
