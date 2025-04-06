using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Persistence;

namespace Repositories.Repository.GeneralRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; }
        public IVariationRepository Variation { get; }
        public IVariationOptionRepository VariationOption { get; }
        public IProductItemRepository ProductItemRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public ISeriesRepository SeriesRepository { get; }

        public UnitOfWork(ApplicationDbContext context,
                          ICategoryRepository category,
                          IVariationRepository variation,
                          IVariationOptionRepository variationOption,
                          IProductItemRepository productItemRepository,
                          IProductRepository productRepository,
                          IBrandRepository brandRepository)
        {
            _context = context;
            Category = category;
            Variation = variation;
            VariationOption = variationOption;
            ProductItemRepository = productItemRepository;
            ProductRepository = productRepository;
            BrandRepository = brandRepository;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
