using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Class_Library.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Meal_Ordering_Customer.Services
{
    public class CustomerService : BaseOrderService
    {
        public CustomerService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<GetMenuRequest> GetMenuAsync(string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetMenuRequest>("api/v1/management/menu");
        }

        public async Task<Category> GetCategoryByIdAsync(string accessToken, int CategoryId, bool IncludeProduct)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<Category>($"api/v1/management/category?CategoryId={CategoryId}&IncludeProduct={IncludeProduct}");
        }

        public async Task<Product> GetProductByIdAsync(string accessToken, int ProductId)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<Product>($"api/v1/management/product?ProductId={ProductId}");
        }

        public async Task<Order> GetOrderByIdAsync(string accessToken, int OrderId)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<Order>($"api/v1/management/orders?OrderId={OrderId}");
        }

        public async Task<GetOrdersRequest> GetOrdersByUsernameAsync(string accessToken, string Username)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetOrdersRequest>($"api/v1/management/orders?Username={Username}");
        }

    }
}
