using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IProductRepository : IGeneralRepository<Product>
    {
        Task<Product?> GetByIdAsync(Guid? id);
        Task<bool> IsProductExistsAsync(string name, CancellationToken cancellationToken);
    }
}
