using FluentValidation;

namespace Application.VariationManagement.Commands.Update
{
    public class UpdateVariationValidation : AbstractValidator<UpdateVariationDto>
    {
        public UpdateVariationValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Khong duoc rong")
                .NotNull().WithMessage("Khong duoc null");
        }
    }
}
