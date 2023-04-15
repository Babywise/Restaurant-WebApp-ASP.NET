using Microsoft.AspNetCore.Identity;

namespace Meal_Ordering_API.Services
{
    public interface IJwtService
    {
        public string GenerateJwtToken(IdentityUser user);
    }
}
