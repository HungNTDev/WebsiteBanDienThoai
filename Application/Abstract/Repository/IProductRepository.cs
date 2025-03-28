using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IProductRepository : IGeneralRepository<Product> 
    {
        Task<bool> IsProductExistsAsync(string name, CancellationToken cancellationToken);
    }
}
