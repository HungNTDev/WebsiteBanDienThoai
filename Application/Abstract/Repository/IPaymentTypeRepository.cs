using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IPaymentTypeRepository : IGeneralRepository<PaymentType>
    {
        Task<PaymentType?> GetByValueAsync(string value);
        Task<PaymentType?> GetByIdAsync(Guid id);
    }
}
