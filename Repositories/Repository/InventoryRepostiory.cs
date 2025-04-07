using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class InventoryRepostiory : GeneralRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepostiory(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Inventory> GetByIdAsync(Guid id)
        {
            return await _context.Inventories
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsInventoryExistsAsync(Guid Id,
            CancellationToken cancellationToken)
        {
            return await GetAll().AnyAsync(c => c.ProductItemId == Id,
                cancellationToken);
        }
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventories.Include(x => x.ProductItem)
                .ToListAsync();
        }
    }
}
