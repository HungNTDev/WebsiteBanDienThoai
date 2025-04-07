using FluentValidation;

namespace Application.SeriesManagement.Commands.Update
{
    public class UpdateSeriesValidation : AbstractValidator<UpdateSeriesDto>
    {
        public UpdateSeriesValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
        }
    }
}
