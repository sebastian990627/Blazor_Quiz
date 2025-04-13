using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor_Quiz.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(string username, string password)
        {
            var loginRequest = new { Username = username, Password = password };
            var response = await _httpClient.GetAsync("token");

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var token = await response.Content.ReadAsStringAsync();
            await _localStorage.SetItemAsync("authToken", token);

            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }

        public async Task<string> GetToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }

        public async Task<bool> RefreshToken()
        {
            // Implement token refresh logic here if needed
            var response = await _httpClient.GetAsync("token"); // Replace with your actual refresh token endpoint
            if (response.IsSuccessStatusCode)
            {
                var newToken = await response.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("authToken", newToken);
                return true;
            }
            return false;
        }

        public async Task<bool> IsTokenExpired()
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token))
                return true;

            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
                return true;

            return jwtToken.ValidTo < DateTime.UtcNow;
        }

        private class AuthResponse
        {
            public string Token { get; set; }
        }
    }
}
