using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Repositories.Repository;
using Repositories.Repository.BaseRepository;
using Repositories.Repository.GeneralRepository;

namespace NET1061_Server.Extensions
{
    public static class RegisterRepositoryExtensions
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();
            services.AddScoped<IUploadHelper, UploadHelper>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IVariationRepository, VariationRepository>();
            services.AddScoped<IVariationOptionRepository, VariationOptionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
