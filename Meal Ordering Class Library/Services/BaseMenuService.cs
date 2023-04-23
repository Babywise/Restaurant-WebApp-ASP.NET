using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Meal_Ordering_Class_Library.Services
{
    public class BaseMenuService : BaseService
    {
        public BaseMenuService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<GetMenuRequest> GetMenuAsync(string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetMenuRequest>("api/v2/menu");
        }

    }
}
