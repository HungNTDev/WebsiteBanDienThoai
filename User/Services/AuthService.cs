using System.Net.Http.Json;
using User.Models;

namespace User.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";
        private const string UserKey = "userInfo";

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<AuthResult> Login(LoginModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);
                var result = await response.Content.ReadFromJsonAsync<AuthResult>();

                if (result?.Success == true)
                {
                    await _localStorage.SetItemAsync(TokenKey, result.Token);
                    // Lưu thông tin user vào localStorage
                    var userInfo = await GetUserInfo(result.Token);
                    await _localStorage.SetItemAsync(UserKey, userInfo);
                }

                return result ?? new AuthResult { Success = false, Message = "Đăng nhập thất bại" };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<AuthResult> Register(RegisterModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", model);
                return await response.Content.ReadFromJsonAsync<AuthResult>() 
                    ?? new AuthResult { Success = false, Message = "Đăng ký thất bại" };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, Message = ex.Message };
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
            await _localStorage.RemoveItemAsync(UserKey);
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            return !string.IsNullOrEmpty(token);
        }

        public async Task<UserInfo> GetCurrentUser()
        {
            return await _localStorage.GetItemAsync<UserInfo>(UserKey) ?? new UserInfo();
        }

        private async Task<UserInfo> GetUserInfo(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("api/auth/user-info");
                return await response.Content.ReadFromJsonAsync<UserInfo>() ?? new UserInfo();
            }
            catch
            {
                return new UserInfo();
            }
        }
    }
} 