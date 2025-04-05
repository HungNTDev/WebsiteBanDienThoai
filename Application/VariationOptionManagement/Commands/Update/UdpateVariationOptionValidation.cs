using Domain.Entities;
using FluentValidation;

namespace Application.VariationOptionManagement.Commands.Update
{
    public class UdpateVariationOptionValidation : AbstractValidator<UpdateVariationOptionDto>
    {
        public UdpateVariationOptionValidation()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
        }
    }
}
