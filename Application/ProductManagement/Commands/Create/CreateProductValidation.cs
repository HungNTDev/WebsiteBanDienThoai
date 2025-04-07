using FluentValidation;

namespace Application.ProductManagement.Commands.Create
{
    public class CreateProductValidation : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Khong duoc null")
                .NotEmpty().WithMessage("Khong duoc empty");
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Khong duoc null")
                .NotEmpty().WithMessage("Khong duoc empty")
                .GreaterThanOrEqualTo(0).WithMessage("Phai lon hon 0");

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Khong duoc null")
                .NotEmpty().WithMessage("Khong duoc empty");
        }
    }
}
