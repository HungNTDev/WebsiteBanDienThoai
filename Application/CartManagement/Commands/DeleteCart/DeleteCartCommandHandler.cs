using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository.Base;
using Application.Abstract.Repository;
using Microsoft.Extensions.Logging;

namespace Application.CartManagement.Commands.DeleteCart
{
    public class DeleteCartCommandHandler : ICommandHandler<DeleteCartCommand, ApiResponse<object>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCartCommandHandler> _logger;

        public DeleteCartCommandHandler(
            ICartRepository cartRepository,
            IUnitOfWork unitOfWork,
            ILogger<DeleteCartCommandHandler> logger)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<object>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _cartRepository.ClearCart(request.model.UserId);
                if (cart == null)
                    return ApiResponseBuilder.Error<object>("Không tìm thấy giỏ hàng");
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>(null, "🛒 Đã xóa toàn bộ sản phẩm trong giỏ hàng");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi xoá giỏ hàng");
                return ApiResponseBuilder.Error<object>("Đã xảy ra lỗi khi xoá giỏ hàng.");
            }
        }
    }

}
