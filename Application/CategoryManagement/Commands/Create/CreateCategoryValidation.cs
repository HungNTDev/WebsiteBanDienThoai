using FluentValidation;

namespace Application.CategoryManagement.Commands.Create
{
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Khong duoc rong")
                .NotNull().WithMessage("Khong duoc null");
        }
    }
}
