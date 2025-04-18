using Application.Abstract.Repository.Base;
using Application.ProductItemManagement.Commands.Create;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IProductItemRepository : IGeneralRepository<ProductItem>
    {
        Task<bool> IsProductItemExistAsync(string name, CancellationToken cancellationToken);
        Task<ProductItem?> GetProductItemByOptionsAsync(Guid productId, List<Guid> optionIds);
        Task<ProductItemDto?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, ProductItem item, List<Guid> variationOptionIds);
        Task<Guid> CreateAsync(ProductItem item, List<Guid> variationOptionIds);
    }
}
