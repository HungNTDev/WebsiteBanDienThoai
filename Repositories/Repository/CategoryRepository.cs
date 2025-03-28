using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class CategoryRepository : GeneralRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> IsCategoryExistsAsync(string categoryName,
             CancellationToken cancellationToken)
        {
            return await GetAll().AnyAsync(c => c.Name == categoryName,
                cancellationToken);
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Categories.Include(c => c.Products)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsUniqueCategoryName(string name)
        {
            var product = await _context.Categories
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return product == null;
        }
    }
}
