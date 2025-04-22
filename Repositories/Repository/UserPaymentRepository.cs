using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class UserPaymentRepository : GeneralRepository<UserPayment>, IUserPaymentRepository
    {
        public UserPaymentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UserPayment?> GetByUserIdAndProviderAsync(Guid userId, string provider)
        {
            return await _context.Set<UserPayment>()
                .FirstOrDefaultAsync(up => up.UserId == userId && up.Provider == provider);
        }

        public async Task<UserPayment?> GetByUserAndTypeAsync(Guid userId, Guid paymentTypeId)
        {
            return await _context.UserPayments
                .FirstOrDefaultAsync(x => x.UserId == userId && x.PaymentTypeId == paymentTypeId);
        }
    }
}
