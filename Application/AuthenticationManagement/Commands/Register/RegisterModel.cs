using System.ComponentModel.DataAnnotations;

namespace Application.AuthenticationManagement.Commands.Register
{
    public class RegisterModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName  { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
