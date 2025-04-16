using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CartManagement.Commands.AddCart
{
    public class CreateCartCommandHandler : ICommandHandler<CreateCartCommand, ApiResponse<object>>
    {
        private readonly ILogger<CreateCartCommandHandler> _logger;
        private readonly ICartRepository _cartRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCartCommandHandler(IUnitOfWork unitOfWork,
                                        IProductItemRepository productItemRepository,
                                        ICartRepository cartRepository,
                                        ILogger<CreateCartCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _productItemRepository = productItemRepository;
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<object>> Handle(CreateCartCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var cart = await _cartRepository.GetByIdAsync(model.UserId);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        Id = Guid.NewGuid(),
                        UserId = model.UserId,
                        CartItems = new List<CartItem>()
                    };
                    _unitOfWork.Entry(cart, EntityState.Added);
                    await _unitOfWork.SaveChangesAsync();
                }
                foreach (var item in model.CartItems)
                {
                    if (item.ProductId.HasValue)
                    {
                        await AddProductToCart(cart, item.ProductId.Value, item.Quantity.Value);
                    }
                }
                _cartRepository.Update(cart);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("",
                   $"Thêm vào giỏ hàng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }

        private async Task<bool> AddProductToCart(Cart cart, Guid productId, int quantity)
        {
            var productItem = await _productItemRepository.GetByIdAsync(productId);
            if (productItem == null) throw new Exception("Sản phẩm không tồn tài");

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductItemId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                _unitOfWork.Entry(cartItem).State = EntityState.Modified;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductItemId = productId,
                    Quantity = quantity,
                    UnitPrice = (decimal)productItem.Price,
                    Image = productItem.Image
                };
                cart.CartItems.Add(newCartItem);
                _unitOfWork.Entry(newCartItem).State = EntityState.Added;
            }
            return true;
        }
    }
}
