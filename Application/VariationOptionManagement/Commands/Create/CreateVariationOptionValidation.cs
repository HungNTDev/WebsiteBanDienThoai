using FluentValidation;

namespace Application.VariationOptionManagement.Commands.Create
{
    public class CreateVariationOptionValidation : AbstractValidator<CreateVariationOptionDto>
    {
        public CreateVariationOptionValidation()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
        }
    }
}
