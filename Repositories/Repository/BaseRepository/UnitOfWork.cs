using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public IUserRepository UserRepository { get; }
        public ICartRepository CartRepository { get; }
        //public IInventoryRepository InventoryRepository { get; }

        public UnitOfWork(ApplicationDbContext context,
                          ICategoryRepository category,
                          IVariationRepository variation,
                          IVariationOptionRepository variationOption,
                          IProductItemRepository productItemRepository,
                          IProductRepository productRepository,
                          IBrandRepository brandRepository,
                          ISeriesRepository seriesRepository,
                          IUserRepository userRepository,
                          ICartRepository cartRepository)
        {
            _context = context;
            Category = category;
            Variation = variation;
            VariationOption = variationOption;
            ProductItemRepository = productItemRepository;
            ProductRepository = productRepository;
            BrandRepository = brandRepository;
            SeriesRepository = seriesRepository;
            UserRepository = userRepository;
            CartRepository = cartRepository;
        }
        public void Entry<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            _context.Entry(entity).State = state;
        }
        public EntityEntry Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return _context.Entry(entity);
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
