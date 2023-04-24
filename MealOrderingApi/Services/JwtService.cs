using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealOrderingApi.Services
{
    public class JwtService : BaseJwtService
    {
        private UserManager<User> _userManager;

        public JwtService(IConfiguration config, UserManager<User> userManager) : base(config)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
            };

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return await Task.FromResult(tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));

        }

    }
}
