using System.ComponentModel.DataAnnotations;

namespace User.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = "";

        public string Address { get; set; } = "";
    }
} 