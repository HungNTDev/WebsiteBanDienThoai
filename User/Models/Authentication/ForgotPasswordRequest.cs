using System.ComponentModel.DataAnnotations;

namespace User.Models.Authentication
{
    public class ForgotPasswordRequest
    {
        public ForgotPasswordModel Model { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
    }
} 