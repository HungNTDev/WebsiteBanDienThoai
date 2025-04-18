using FluentValidation;

namespace Application.CartManagement.Commands.DeleteCartItem
{
    public class DeleteCartItemValidator : AbstractValidator<DeleteCartItemDto>
    {
        public DeleteCartItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Vui lòng chọn sản phẩm để xóa khỏi giỏ hàng.");
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Vui lòng cung cấp thông tin người dùng.");
        }
    }
}
