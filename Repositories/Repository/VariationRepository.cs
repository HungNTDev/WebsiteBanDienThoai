using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class VariationRepository : GeneralRepository<Variation>, IVariationRepository
    {
        public VariationRepository(ApplicationDbContext context) : base(context)
        { }
        public async Task<Variation> GetByIdAsync(Guid id)
        {
            return await _context.Variations.Include(c => c.VariationOption)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsVariationExistsAsync(string variationName,
            CancellationToken cancellationToken)
        {
            return await GetAll().AnyAsync(c => c.Name == variationName,
                cancellationToken);
        }
    }
}
