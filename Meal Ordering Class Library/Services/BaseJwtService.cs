using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace Meal_Ordering_Class_Library.Services
{
    public class BaseJwtService
    {
        public readonly IConfiguration _config;

        public BaseJwtService(IConfiguration config)
        {
            _config = config;
        }


        public async Task<string>? GetClaimValueFromToken(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = await Task.Run(() => tokenHandler.ReadToken(token) as JwtSecurityToken);

            if (jwtToken == null)
            {
                return null;
            }

            return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
        //Not used yet
        public async Task<List<string>> GetRoleClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = await Task.Run(() => tokenHandler.ReadToken(token) as JwtSecurityToken);

            if (jwtToken == null)
            {
                return null;
            }

            var roleClaims = jwtToken.Claims
                .Where(c => c.Type == "role")
                .Select(c => c.Value)
                .ToList();

            return roleClaims;
        }

        public async Task<bool> CheckCustomerRoleClaimFromToken(string token)
        {
            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = await Task.Run(() => tokenHandler.ReadToken(token) as JwtSecurityToken);

                if (jwtToken == null)
                {
                    return false;
                }

                var roleClaims = jwtToken.Claims
                    .Where(c => c.Type == "role")
                    .Select(c => c.Value)
                    .ToList();

                foreach (var role in roleClaims)
                {
                    if (role.CompareTo("Customer") == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> CheckRestaurantRoleClaimFromToken(string token)
        {
            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = await Task.Run(() => tokenHandler.ReadToken(token) as JwtSecurityToken);

                if (jwtToken == null)
                {
                    return false;
                }

                var roleClaims = jwtToken.Claims
                .Where(c => c.Type == "role")
                .Select(c => c.Value)
                .ToList();

                foreach (var role in roleClaims)
                {
                    if (role.CompareTo("Restaurant") == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<DateTime?> GetExpiryFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = await Task.Run(() => tokenHandler.ReadToken(token) as JwtSecurityToken);

            if (jwtToken == null)
            {
                return null;
            }

            var expiryClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");
            if (expiryClaim != null && long.TryParse(expiryClaim.Value, out var expiryTimeUnix))
            {
                var expiryTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(expiryTimeUnix);
                //offest for utc + 5 (EST)
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

                expiryTimeUtc = TimeZoneInfo.ConvertTimeFromUtc(expiryTimeUtc, easternTimeZone);

                return expiryTimeUtc;
            }
            return null;
        }

    }
}
