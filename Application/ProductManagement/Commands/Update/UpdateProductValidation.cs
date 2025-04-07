using FluentValidation;

namespace Application.ProductManagement.Commands.Update
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Khong duoc null")
                .NotEmpty().WithMessage("Khong duoc empty");
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Khong duoc null")
                .NotEmpty().WithMessage("Khong duoc empty")
                .GreaterThanOrEqualTo(0).WithMessage("Phai lon hon 0");

        }
    }
}
