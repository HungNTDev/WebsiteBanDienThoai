using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CartManagement.Commands.UpdateCart
{
    public class UpdateCartCommandHandler : ICommandHandler<UpdateCartCommand, ApiResponse<object>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCartCommandHandler> _logger;

        public UpdateCartCommandHandler(IUnitOfWork unitOfWork,
                                        IMapper mapper,
                                        ICartRepository cartRepository,
                                        ILogger<UpdateCartCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<object>> Handle(UpdateCartCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.model.CartItems)
                {
                    var cartItem = await _cartRepository.GetCartItemByIdAsync(item.Id);
                    if (cartItem == null)
                    {
                        _logger.LogWarning($"Không tìm thấy CartItem với ID: {item.Id}");
                        continue;
                    }
                    if (item.ProductId.HasValue && cartItem.ProductItemId != item.ProductId)
                    {
                        _logger.LogWarning($"CartItem ID {item.Id} không khớp với ProductId {item.ProductId}");
                        continue;
                    }

                    var cart = await _cartRepository.GetCartByIdAsync(cartItem.CartId);
                    if (cart == null)
                    {
                        _logger.LogWarning($"Không tìm thấy Cart với ID: {cartItem.CartId}");
                        continue;
                    }
                    if (item.Quantity <= 0)
                    {
                        cart.CartItems.Remove(cartItem);
                        _unitOfWork.Entry(cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        cartItem.Quantity = item.Quantity;
                        var entry = _unitOfWork.Entry(cartItem);
                        if (entry.State == EntityState.Detached)
                        {
                            _unitOfWork.Entry(cartItem).State = EntityState.Modified;
                        }
                    }
                }
                var affectedRows = await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", $"Cập nhật giỏ hàng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }
    }
}
