using Application.Abstract.Repository;
using Application.ProductItemManagement.Commands.Create;
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
               .Where(pi => pi.ProductConfigs.Count == optionIds.Count
                && optionIds.All(id => pi.ProductConfigs.Any(pc => pc.VariationOptionId == id)))
               .Include(pi => pi.ProductConfigs)
                   .ThenInclude(pc => pc.VariationOption)
                       .ThenInclude(vo => vo.Variation)
               .FirstOrDefaultAsync();
        }

        public async Task<Guid> CreateAsync(ProductItem item, List<Guid> variationOptionIds)
        {
            _context.ProductItems.Add(item);
            await _context.SaveChangesAsync();

            foreach (var optionId in variationOptionIds)
            {
                _context.ProductConfigs.Add(new ProductConfig
                {
                    ProductItemId = item.Id,
                    VariationOptionId = optionId
                });
            }
            await _context.SaveChangesAsync();

            return item.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, ProductItem item, List<Guid> variationOptionIds)
        {
            var existing = await _context.ProductItems.FindAsync(id);
            if (existing == null) return false;

            existing.SKU = item.SKU;
            existing.Price = item.Price;
            existing.Name = item.Name;
            existing.Image = item.Image;
            existing.ProductId = item.ProductId;

            // Remove old configs
            var oldConfigs = await _context.ProductConfigs
               .Where(pc => pc.ProductItemId == id)
               .ToListAsync();

            _context.ProductConfigs.RemoveRange(oldConfigs);
            await _context.SaveChangesAsync();
            // Add new configs
            foreach (var optionId in variationOptionIds)
            {
                _context.ProductConfigs.Add(new ProductConfig
                {
                    ProductItemId = id,
                    VariationOptionId = optionId
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductItemDto?> GetByIdAsync(Guid id)
        {
            var item = await _context.ProductItems
                .Include(pi => pi.Product)
                .Include(pi => pi.ProductConfigs)
                    .ThenInclude(pc => pc.VariationOption)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null) return null;

            return new ProductItemDto
            {
                Id = item.Id,
                SKU = item.SKU,
                Price = item.Price,
                ProductName = item.Product?.Name ?? "",
                VariationOptions = item.ProductConfigs.Select(c => c.VariationOptionId).ToList()
            };
        }
    }
}
