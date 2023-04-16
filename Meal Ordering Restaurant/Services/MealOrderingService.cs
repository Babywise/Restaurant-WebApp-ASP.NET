using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Meal_Ordering_Restaurant.Services
{
    public class MealOrderingService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        public MealOrderingService(IConfiguration config, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_config["ApiSettings:ApiBaseUrl"]);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> LoginAsync(AccountLoginRequest accountLoginRequest)
        {
            return await _httpClient.PostAsJsonAsync("api/v1/account/login", accountLoginRequest);
        }

        public async Task<HttpResponseMessage> RegisterAsync(AccountRegisterRequest accountRegisterRequest)
        {
            return await _httpClient.PostAsJsonAsync("api/v1/account/register", accountRegisterRequest);
        }

        public async Task<HttpResponseMessage> UpdateUserDetailsAsync(AccountEditRequest accountEditRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PutAsJsonAsync("api/v1/account/edit", accountEditRequest);
        }

        public async Task<AccountEditRequest> GetAccountDetails(string username, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<AccountEditRequest>($"api/v1/account/edit?Username={username}");
        }
        private void SetAccessToken(HttpClient httpClient, string accessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

    }
}
