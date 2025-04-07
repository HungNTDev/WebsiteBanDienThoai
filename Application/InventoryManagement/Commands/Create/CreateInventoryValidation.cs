using FluentValidation;

namespace Application.InventoryManagement.Commands.Create
{
    public class CreateInventoryValidation : AbstractValidator<CreateInventoryDto>
    {
        public CreateInventoryValidation()
        {
            RuleFor(x => x.StockQuantity)
                .NotEmpty()
                .WithMessage("Stock quantity is required.")
                .GreaterThan(0)
                .WithMessage("Stock quantity must be greater than 0.");
        }
    }
}
