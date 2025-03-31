using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class VariationOptionRepository : GeneralRepository<VariationOption>,
        IVariationOptionRepository
    {
        public VariationOptionRepository(ApplicationDbContext context) : base(context)
        { }
        public async Task<VariationOption> GetByIdAsync(Guid id)
        {
            return await _context.VariationOptions.Include(c => c.ProductConfigs)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
