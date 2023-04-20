using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Meal_Ordering_Restaurant.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public AccountService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_config["ApiSettings:ApiBaseUrl"]);
        }

        public async Task<HttpResponseMessage> LoginAsync(AccountRequest accountRequest)
        {
            return await _httpClient.PostAsJsonAsync("api/v1/account/login", accountRequest);
        }

        public async Task<HttpResponseMessage> RegisterAsync(AccountRequest accountRequest)
        {
            return await _httpClient.PostAsJsonAsync("api/v1/account/register", accountRequest);
        }

        public async Task<HttpResponseMessage> UpdateUserDetailsAsync(AccountRequest accountRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PutAsJsonAsync("api/v1/account/edit", accountRequest);
        }

        public async Task<AccountRequest> GetAccountDetailsAsync(string username, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<AccountRequest>($"api/v1/account/edit?Username={username}");
        }
        private void SetAccessToken(HttpClient httpClient, string accessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

    }
}
