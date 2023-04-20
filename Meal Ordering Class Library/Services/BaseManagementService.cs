using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Meal_Ordering_Class_Library.Services
{
    public class BaseManagementService : BaseService
    {
        public BaseManagementService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<GetMenuRequest> GetMenuAsync(string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetMenuRequest>("api/v1/management/menu");
        }
    }
}
