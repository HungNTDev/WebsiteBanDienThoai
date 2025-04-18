using FluentValidation;

namespace Application.AuthenticationManagement.Commands.Google
{
    public class GoogleLoginRequestValidation : AbstractValidator<GoogleLoginRequestDto>
    {
        public GoogleLoginRequestValidation()
        {
            RuleFor(x => x.IdToken)
                .NotEmpty()
                .WithMessage("IdToken is required.");
        }
    }
}
