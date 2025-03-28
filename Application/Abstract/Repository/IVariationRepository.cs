using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IVariationRepository : IGeneralRepository<Variation>
    {
        Task<Variation> GetByIdAsync(Guid id);
        Task<bool> IsVariationExistsAsync(string variationName,
            CancellationToken cancellationToken);
    }
}
