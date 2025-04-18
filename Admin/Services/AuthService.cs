using Blazored.LocalStorage;
using System.Net.Http.Json;
using Admin.Models;
using Admin.Models.Authentication;

namespace Admin.Services
{
    public interface IAuthService
    {
        Task<bool> IsAuthenticated();
        Task<string> Login(string email, string password);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private const string LOGIN_ENDPOINT = "api/Authentication/login";

        public AuthService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string> Login(string email, string password)
        {
            var request = new LoginRequest
            {
                Model = new LoginModel
                {
                    Email = email,
                    Password = password,
                    ReturnUrl = "string"
                }
            };

            var response = await _httpClient.PostAsJsonAsync(LOGIN_ENDPOINT, request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Login failed");
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
            if (!result.IsSuccess)
            {
                throw new Exception(result.Message ?? "Login failed");
            }

            await _localStorage.SetItemAsync("token", result.Data);
            return result.Message;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
        }
    }
}