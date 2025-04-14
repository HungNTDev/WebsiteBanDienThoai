using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IUserRepository : IGeneralRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByIdAsync(Guid id);
        Task<ApplicationUser> UpdateAsync(ApplicationUser user);
    }
}
