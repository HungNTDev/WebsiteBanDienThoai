using System.ComponentModel.DataAnnotations;

namespace Application.AuthenticationManagement.Commands.ForgotPassword
{
    public class ForgotPasswordModel
    {
        [EmailAddress]
        public string? Email { get; set; }
    }
}
