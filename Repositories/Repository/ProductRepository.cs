using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class ProductRepository : GeneralRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsProductExistsAsync(string name,
            CancellationToken cancellationToken)
        {
            return await GetAll().AnyAsync(c => c.Name == name, cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid? id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.ProductConfigs)
                        .ThenInclude(pc => pc.VariationOption)
                            .ThenInclude(vo => vo.Variation)
                .FirstOrDefaultAsync();
        }
    }
}
