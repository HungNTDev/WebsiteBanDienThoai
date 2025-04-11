namespace Application.Abstract.Repository.Base
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IVariationRepository Variation { get; }
        IProductItemRepository ProductItemRepository { get; }
        IProductRepository ProductRepository { get; }
        IVariationOptionRepository VariationOption { get; }
        IBrandRepository BrandRepository { get; }
        ISeriesRepository SeriesRepository { get; }
        //IInventoryRepository InventoryRepository { get; }
        void Update<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
