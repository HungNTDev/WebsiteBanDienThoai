using User.Models;

namespace User.Services
{
    public interface IAuthService
    {
        Task<AuthResult> Login(LoginModel model);
        Task<AuthResult> Register(RegisterModel model);
        Task Logout();
        Task<bool> IsAuthenticated();
        Task<UserInfo> GetCurrentUser();
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string Token { get; set; } = "";
    }
} 