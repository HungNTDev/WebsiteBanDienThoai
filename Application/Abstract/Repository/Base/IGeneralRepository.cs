using Domain.Abstract;

namespace Application.Abstract.Repository.Base
{
    public  interface IGeneralRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAll();
        Task<TEntity> CreateAsync(TEntity entity);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
