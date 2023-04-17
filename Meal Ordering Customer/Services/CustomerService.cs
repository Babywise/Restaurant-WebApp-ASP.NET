using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Meal_Ordering_Customer.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public CustomerService(IConfiguration config, HttpClient httpClient)
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
        public async Task<Category> GetCategoryAsync(string accessToken, int CategoryId)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<Category>($"api/v1/management/category?CategoryId={CategoryId}");
        }
        public async Task<GetOrdersRequest> GetOrdersAsync(string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetOrdersRequest>("api/v1/management/orders");
        }

        private void SetAccessToken(HttpClient httpClient, string accessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

    }
}
