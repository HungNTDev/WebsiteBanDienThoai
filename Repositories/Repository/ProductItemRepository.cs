using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class ProductItemRepository : GeneralRepository<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsProductItemExistAsync(string name, CancellationToken cancellationToken)
        {
            return await GetAll().AnyAsync(c => c.SKU == name, cancellationToken);
        }

        public async Task<ProductItem?> GetProductItemByOptionsAsync(Guid productId, List<Guid> optionIds)
        {
            return await _context.ProductItems
               .Where(pi => pi.ProductId == productId)
               .Where(pi => pi.ProductConfigs
                   .Select(pc => pc.VariationOptionId)
                   .Count() == optionIds.Count
                   && pi.ProductConfigs
                       .All(pc => optionIds.Contains(pc.VariationOptionId)))
               .Include(pi => pi.ProductConfigs)
                   .ThenInclude(pc => pc.VariationOption)
                       .ThenInclude(vo => vo.Variation)
               .FirstOrDefaultAsync();
        }
    }
}
