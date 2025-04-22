using FluentValidation;

namespace Application.PaymentManagement.Commands.Create
{
    public class CreateVnPayPaymentUrlValidator : AbstractValidator<CreateVnPayPaymentUrlDto>
    {
        public CreateVnPayPaymentUrlValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId is required.")
                .NotEqual(Guid.Empty).WithMessage("OrderId cannot be empty.");

        }
    }
}
