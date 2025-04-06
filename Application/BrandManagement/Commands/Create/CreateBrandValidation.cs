using FluentValidation;

namespace Application.BrandManagement.Commands.Create
{
    public class CreateBrandValidation : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
        }
    }
}
