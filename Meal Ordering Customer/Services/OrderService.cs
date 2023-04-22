using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Class_Library.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Meal_Ordering_Customer.Services
{
    public class OrderService : BaseOrderService
    {
        public OrderService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<Category> GetCategoryByIdAsync(string accessToken, int CategoryId, bool IncludeProduct)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<Category>($"api/v2/management/category?CategoryId={CategoryId}&IncludeProduct={IncludeProduct}");
        }

        public async Task<Product> GetProductByIdAsync(string accessToken, int ProductId)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<Product>($"api/v2/management/product?ProductId={ProductId}");
        }

        public async Task<GetOrdersRequest> GetOrdersByUsernameAsync(string accessToken, string Username)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetOrdersRequest>($"api/v2/order/orders?Username={Username}");
        }

    }
}
