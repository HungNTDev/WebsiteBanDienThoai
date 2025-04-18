using User.Models;
using User.Models.Authentication;

namespace User.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> LoginAsync(LoginModel model);
        Task<AuthResult> Register(RegisterModel model);
        Task Logout();
        Task<bool> IsAuthenticated();
        Task<UserInfo> GetCurrentUser();
        Task<UserInfo?> GetCurrentUserAsync();
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string Token { get; set; } = "";
    }
}