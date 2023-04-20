using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace Meal_Ordering_Class_Library.Services
{
    public abstract class BaseService
    {
        public readonly HttpClient _httpClient;
        public readonly IConfiguration _config;
        public BaseService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_config["ApiSettings:ApiBaseUrl"]);
        }
        public void SetAccessToken(HttpClient httpClient, string accessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
