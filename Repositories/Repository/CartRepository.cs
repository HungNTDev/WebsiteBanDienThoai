using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class CartRepository : GeneralRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context) { }
        public IQueryable<Cart> GetAll() => _context.Carts;

        public async Task<Cart> GetByIdAsync(Guid id)
        {
            return await _context.Carts.AsTracking()
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.ProductItem)
                .FirstOrDefaultAsync(c => c.UserId == id);
        }

        public async Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemId);
        }
        public async Task<bool> DeleteCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemId);

            if (cartItem == null)
            {
                return false; // Không tìm thấy sản phẩm trong giỏ hàng
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCart(Guid id)
        {
            var cart = await _context.Carts.Where(x => x.UserId == id)
                .Include(c => c.CartItems).FirstOrDefaultAsync();

            if (cart == null || !cart.CartItems.Any())
            {
                return false;
            }
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> GetCartByIdAsync(Guid cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems).ThenInclude(p => p.ProductItem).Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.ProductItem).Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }


        public void UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
        }
    }
}
