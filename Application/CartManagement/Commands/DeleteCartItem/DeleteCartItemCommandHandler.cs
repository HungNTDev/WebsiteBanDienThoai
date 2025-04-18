using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.CartManagement.Commands.DeleteCartItem
{
    public class DeleteCartItemCommandHandler : ICommandHandler<DeleteCartItemCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCartItemCommandHandler> _logger;
        private readonly IValidator<DeleteCartItemDto> _validator;
        private readonly ICartRepository _cartRepository;
        public DeleteCartItemCommandHandler(ILogger<DeleteCartItemCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IValidator<DeleteCartItemDto> validator,
            ICartRepository cartRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _cartRepository = cartRepository;
        }
        public async Task<ApiResponse<object>> Handle(DeleteCartItemCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var cart = await _cartRepository.GetByIdAsync(request.model.UserId);
                if (cart == null)
                    return ApiResponseBuilder.Error<object>("Không tìm thấy giỏ hàng");

                var item = cart.CartItems.FirstOrDefault(x => x.ProductItemId == request.model.ProductId);
                if (item == null)
                    return ApiResponseBuilder.Error<object>("Sản phẩm không có trong giỏ hàng");

                cart.CartItems.Remove(item);
                _cartRepository.Update(cart);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("",
                    $"Xóa sản phẩm khỏi giỏ hàng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An error occurred while deleting cart item for ProductId: {ProductId}",
                    model.ProductId);
                throw;
            }
        }
    }
}
