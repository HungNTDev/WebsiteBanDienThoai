using FluentValidation;

namespace Application.ProductItemManagement.Commands.Update
{
    public class UpdateProductItemValidation : AbstractValidator<UpdateProductItemDto>
    {
        public UpdateProductItemValidation()
        {
            RuleFor(x => x.SKU)
                .NotEmpty()
                .WithMessage("SKU is required.")
                .MaximumLength(50)
                .WithMessage("SKU must not exceed 50 characters.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");
        }
    }
}
