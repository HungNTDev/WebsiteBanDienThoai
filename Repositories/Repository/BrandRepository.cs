using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class BrandRepository : GeneralRepository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        { }
        public async Task<bool> IsBrandExistsAsync(string name)
        {
            return await GetAll().AnyAsync(c => c.Name == name);
        }
        public async Task<Brand> GetByIdAsync(Guid id)
        {
            return await _context.Brands.AsNoTracking().Include(c => c.Series)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
