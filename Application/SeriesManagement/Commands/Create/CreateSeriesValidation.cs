using FluentValidation;

namespace Application.SeriesManagement.Commands.Create
{
    public class CreateSeriesValidation : AbstractValidator<CreateSeriesDto>
    {
        public CreateSeriesValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(100).WithMessage("Tên không được quá 100 ký tự");
        }
    }
}
