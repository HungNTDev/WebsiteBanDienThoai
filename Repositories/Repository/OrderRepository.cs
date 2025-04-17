using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class OrderRepository : GeneralRepository<Order>
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }
        public IQueryable<Order> GetAll() => _context.Orders;
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.AsTracking()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductItem)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Order> GetOrderByUserIdAsync(Guid userId)
        {
            return await _context.Orders.AsTracking()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductItem)
                .FirstOrDefaultAsync(o => o.UserId == userId);
        }

    }
}
