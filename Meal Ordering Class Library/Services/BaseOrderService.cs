using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Meal_Ordering_Class_Library.Services
{
    public class BaseOrderService : BaseService
    {
        public BaseOrderService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<HttpResponseMessage> UpdateOrderAsync(string accessToken, UpdateOrderRequest updateOrderRequest)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PutAsJsonAsync("api/v2/order/update-order", updateOrderRequest);
        }
    }
}
