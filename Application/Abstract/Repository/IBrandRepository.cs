using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IBrandRepository : IGeneralRepository<Domain.Entities.Brand>
    {
        Task<Brand> GetByIdAsync(Guid id);
        Task<bool> IsBrandExistsAsync(string name);
        Task<List<Product>> GetProductsByBrandIdAsync(Guid brandId);
    }
}
