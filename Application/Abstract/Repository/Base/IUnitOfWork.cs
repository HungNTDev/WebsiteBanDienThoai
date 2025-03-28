namespace Application.Abstract.Repository.Base
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IVariationRepository Variation { get; }
        IProductItemRepository ProductItemRepository { get; }
        IProductRepository ProductRepository { get; }
        void Update<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
