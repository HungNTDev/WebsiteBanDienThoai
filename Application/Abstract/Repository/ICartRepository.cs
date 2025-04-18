using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface ICartRepository : IGeneralRepository<Cart>
    {
        IQueryable<Cart> GetAll();
        Task<Cart> GetByIdAsync(Guid id);
        Task<bool> ClearCart(Guid id);
        Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId);
        void UpdateCartItem(CartItem cartItem);
        Task<Cart> GetCartByIdAsync(Guid cartId);
        Task<bool> DeleteCartItemAsync(Guid cartItemId);
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
    }
}
