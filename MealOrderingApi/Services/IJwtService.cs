using Meal_Ordering_Class_Library.Entities;

namespace MealOrderingApi.Services
{
    public interface IJwtService
    {
        public Task<string> GenerateJwtToken(User user);
        public Task<string>? GetClaimValueFromToken(string token, string claimType);
    }
}
