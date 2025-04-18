using FluentValidation;

namespace Application.OrderManagement.Commands.Create
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
        }
    }
}
