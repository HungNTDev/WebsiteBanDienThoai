using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class PaymentTypeRepository : GeneralRepository<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PaymentType?> GetByValueAsync(string value)
        {
            return await _context.Set<PaymentType>()
                .FirstOrDefaultAsync(pt => pt.Value == value);
        }
        public async Task<PaymentType?> GetByIdAsync(Guid id)
        {
            return await _context.PaymentTypes.FindAsync(id);
        }
    }
}
