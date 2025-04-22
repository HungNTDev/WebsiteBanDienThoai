using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.GeneralRepository
{
    public class PaymentRepostiory : GeneralRepository<Payment>, IPaymentRepository
    {
        public PaymentRepostiory(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _context.Payments
                .Include(p => p.UserPayment)
                .ThenInclude(up => up.PaymentType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.Payments
                .Where(p => p.OrderId == orderId)
                .Include(p => p.UserPayment)
                .ThenInclude(up => up.PaymentType)
                .ToListAsync();
        }
    }
}
