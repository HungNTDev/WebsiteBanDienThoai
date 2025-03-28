using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IProductItemRepository : IGeneralRepository<ProductItem>
    {
        Task<bool> IsProductItemExistAsync(string name, CancellationToken cancellationToken);
        Task<ProductItem?> GetProductItemByOptionsAsync(Guid productId, List<Guid> optionIds);
    }
}
