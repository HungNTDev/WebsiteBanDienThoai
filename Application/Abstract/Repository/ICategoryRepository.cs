using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface ICategoryRepository : IGeneralRepository<Category>
    {
        Task<bool> IsCategoryExistsAsync(string categoryName, CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(Guid id);
        Task<bool> IsUniqueCategoryName(string name);
    }
}
