using System.ComponentModel.DataAnnotations;

namespace Application.AuthenticationManagement.Commands.ResetPassword
{
    public class ResetPasswordModels
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
