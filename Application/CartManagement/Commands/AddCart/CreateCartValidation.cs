using FluentValidation;

namespace Application.CartManagement.Commands.AddCart
{
    public class CreateCartValidation
        : AbstractValidator<CreateCartDto>
    {
        public CreateCartValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
