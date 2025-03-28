using FluentValidation;

namespace Application.AuthenticationManagement.Commands.ResetPassword
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModels>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email không được để trống")
              .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(8).WithMessage("Mật khẩu phải có ít nhất 8 ký tự.")
                .MaximumLength(255).WithMessage("Mật khẩu không được vượt quá 255 ký tự.")
                .Matches(@"[A-Z]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết hoa.")
                .Matches(@"[a-z]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết thường.")
                .Matches(@"[0-9]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ số.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Mật khẩu phải chứa ít nhất một ký tự đặc biệt như (!? *.).");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Mật khẩu xác nhận không được để trống")
                .Equal(x => x.NewPassword).WithMessage("Mật khẩu xác nhận không khớp với mật khẩu");
        }
    }
}
