using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IUserPaymentRepository : IGeneralRepository<UserPayment>
    {
        Task<UserPayment?> GetByUserIdAndProviderAsync(Guid userId, string provider);
        Task<UserPayment?> GetByUserAndTypeAsync(Guid userId, Guid paymentTypeId);
    }
}
