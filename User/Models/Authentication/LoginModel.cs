using System.ComponentModel.DataAnnotations;

namespace User.Models.Authentication
{

    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
