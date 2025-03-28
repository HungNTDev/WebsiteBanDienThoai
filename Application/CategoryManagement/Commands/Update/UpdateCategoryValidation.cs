using FluentValidation;

namespace Application.CategoryManagement.Commands.Update
{
    public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bắt buộc phải có tên");

        }
    }
}
