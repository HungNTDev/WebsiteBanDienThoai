using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class UserRepository : GeneralRepository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            return await _context.Users.AsTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _context.Users.AsTracking()
               .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
