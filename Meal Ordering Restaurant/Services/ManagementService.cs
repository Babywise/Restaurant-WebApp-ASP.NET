using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Meal_Ordering_Restaurant.Services
{
    public class ManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ManagementService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_config["ApiSettings:ApiBaseUrl"]);
        }

        public async Task<GetMenuRequest> GetMenuAsync(string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetMenuRequest>("api/v1/management/menu");
        }
        public async Task<GetOrdersRequest> GetOrdersAsync(string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetOrdersRequest>("api/v1/management/orders");
        }

        public async Task<HttpResponseMessage> AddCategoryAsync(AddCategoryRequest addCategoryRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PostAsJsonAsync("api/v1/management/add-category", addCategoryRequest);
        }

        private void SetAccessToken(HttpClient httpClient, string accessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

    }
}
