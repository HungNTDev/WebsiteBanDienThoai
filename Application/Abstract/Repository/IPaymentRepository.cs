using Application.Abstract.Repository.Base;

namespace Application.Abstract.Repository
{
    public interface IPaymentRepository : IGeneralRepository<Domain.Entities.Payment>
    {
        Task<Domain.Entities.Payment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entities.Payment>> GetByOrderIdAsync(Guid orderId);
    }
}
