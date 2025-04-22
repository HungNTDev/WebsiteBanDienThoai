using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Repositories.GeneralRepository;
using Repositories.GeneralRepository.GeneralRepository;
using Repositories.Repository;
using Repositories.Repository.BaseRepository;

namespace NET1061_Server.Extensions
{
    public static class RegisterRepositoryExtensions
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IVariationRepository, VariationRepository>();
            services.AddScoped<IVariationOptionRepository, VariationOptionRespository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUploadHelper, UploadHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddScoped<IUserPaymentRepository, UserPaymentRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepostiory>();
            //services.AddScoped<IInventoryRepository, InventoryRepostiory>();
            return services;
        }
    }
}
