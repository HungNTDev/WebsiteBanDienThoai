using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class SeriesRepository : GeneralRepository<Series>, ISeriesRepository
    {
        private readonly ApplicationDbContext _context;
        public SeriesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Series?> GetByIdAsync(Guid id)
        {
            return await _context.Series.Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Series>> GetAllAsync()
        {
            return await _context.Series
                .Include(x => x.Products)
                .ToListAsync();
        }
    }
}
