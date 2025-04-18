using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Abstract.Repository.Base
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository Category { get; }
        IVariationRepository Variation { get; }
        IProductItemRepository ProductItemRepository { get; }
        IProductRepository ProductRepository { get; }
        IVariationOptionRepository VariationOption { get; }
        IBrandRepository BrandRepository { get; }
        ISeriesRepository SeriesRepository { get; }
        ICartRepository CartRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        //IInventoryRepository InventoryRepository { get; }
        void Update<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Entry<TEntity>(TEntity entity, EntityState state) where TEntity : class;
        EntityEntry Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
