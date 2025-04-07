using FluentValidation;

namespace Application.InventoryManagement.Commands.Update
{
    public class UpdateInventoryValidation : AbstractValidator<UpdateInventoryDto>
    {
        public UpdateInventoryValidation()
        {
            RuleFor(x => x.ProductItemId)
                .NotEmpty()
                .WithMessage("Product Item ID is required.");
            RuleFor(x => x.StockQuantity)
                .NotEmpty()
                .WithMessage("Stock Quantity is required.")
                .GreaterThan(0)
                .WithMessage("Stock Quantity must be greater than 0.");
            RuleFor(x => x.ReservedQuantity)
                .NotEmpty()
                .WithMessage("Reserved Quantity is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Reserved Quantity must be greater than or equal to 0.");
        }
    }
}
