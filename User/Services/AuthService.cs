using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using User.Models;
using User.Models.Authentication;

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

        public async Task<ApiResponse<string>> LoginAsync(LoginModel model)
        {
            var request = new LoginRequest
            {
                Model = model
            };

            var response = await _httpClient.PostAsJsonAsync("api/Authentication/login", request);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = "Lỗi kết nối đến máy chủ."
                };
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

            if (result == null || !result.IsSuccess || string.IsNullOrEmpty(result.Data))
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = result?.Message ?? "Đăng nhập thất bại."
                };
            }

            // Lưu token vào localStorage
            await _localStorage.SetItemAsync("authToken", result.Data);

            return new ApiResponse<string>
            {
                IsSuccess = true,
                Message = result.Message,
                Data = result.Data
            };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
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



        public async Task<bool> IsAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            return !string.IsNullOrEmpty(token);
        }

        public async Task<UserInfo> GetCurrentUser()
        {
            return await _localStorage.GetItemAsync<UserInfo>(UserKey) ?? new UserInfo();
        }

        //private async Task<UserInfo> GetUserInfo(string token)
        //{
        //    try
        //    {
        //        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        var response = await _httpClient.GetAsync("api/auth/user-info");
        //        return await response.Content.ReadFromJsonAsync<UserInfo>() ?? new UserInfo();
        //    }
        //    catch
        //    {
        //        return new UserInfo();
        //    }
        //}
        public async Task<UserInfo?> GetCurrentUserAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token)) return null;

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt;

            try
            {
                jwt = handler.ReadJwtToken(token);
            }
            catch
            {
                return null;
            }

            var email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var name = jwt.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            if (string.IsNullOrEmpty(email)) return null;

            return new UserInfo
            {
                Email = email,
                FullName = name
            };
        }
    }
}