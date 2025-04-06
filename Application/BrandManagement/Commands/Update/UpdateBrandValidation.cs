using FluentValidation;

namespace Application.BrandManagement.Commands.Update
{
    public class UpdateBrandValidation : AbstractValidator<UpdateBrandDto>
    {
        public UpdateBrandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Value is required.")
                .MaximumLength(100)
                .WithMessage("Value must not exceed 100 characters.");
        }
    }
}
