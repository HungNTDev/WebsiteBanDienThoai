using Application.Abstract.Repository.Base;
using Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Repositories.Repository.GeneralRepository
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext _context;
        public readonly DbSet<TEntity> _entity;

        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
            return entity;
        }

        public TEntity Create(TEntity entity)
        {
            _entity.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _entity.Update(entity);
            return entity;
        }

        public bool Delete(TEntity entity)
        {
            _entity.Remove(entity);
            return true;
        }
    }
}
