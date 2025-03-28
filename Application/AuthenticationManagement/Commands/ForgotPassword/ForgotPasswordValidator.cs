using FluentValidation;

namespace Application.AuthenticationManagement.Commands.ForgotPassword
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email không được để trống")
               .EmailAddress().WithMessage("Email không hợp lệ");
        }
    }
}
