using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class VariationOptionRespository : GeneralRepository<VariationOption>, IVariationOptionRepository
    {
        public VariationOptionRespository(ApplicationDbContext context) : base(context)
        { }

        public async Task<VariationOption> GetByIdAsync(Guid id)
        {
            return await _context.VariationOptions.Include(c => c.ProductConfigs)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsVariationExistsAsync(string variationName,
            CancellationToken cancellationToken)
        {
            return await GetAll().AnyAsync(c => c.Value == variationName,
                cancellationToken);
        }

        public async Task<List<VariationOption>> GetByVariationIdsAsync(List<Guid> variationIds, CancellationToken cancellationToken)
        {
            return await _context.VariationOptions
                .Where(vo => variationIds.Contains(vo.VariationId))
                .ToListAsync(cancellationToken);
        }

    }
}
