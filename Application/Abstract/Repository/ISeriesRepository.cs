using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface ISeriesRepository : IGeneralRepository<Series>
    {
        Task<List<Series>> GetAllAsync();
        Task<Series?> GetByIdAsync(Guid id);
    }
}
