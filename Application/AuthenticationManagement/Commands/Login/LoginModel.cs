using System.ComponentModel.DataAnnotations;

namespace Application.AuthenticationManagement.Commands.Login
{
    public class LoginModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
