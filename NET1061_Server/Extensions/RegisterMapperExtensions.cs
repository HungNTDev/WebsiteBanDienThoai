using Application.Abstract.MapperProfile;
using AutoMapper;

namespace NET1061_Server.Extensions
{
    public static class RegisterMapperExtensions
    {
        public static IServiceCollection AddRegistrationMapper
            (this IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<AuthenticationProfile>();
                c.AddProfile<CategoryProfile>();
                c.AddProfile<VariationProfile>();
                c.AddProfile<VariationOptionProfile>();
                c.AddProfile<ProductProfile>();
                c.AddProfile<BrandProfile>();
                c.AddProfile<SeriesProfile>();
                c.AddProfile<InventoryProfile>();
                c.AddProfile<ProductItemProfile>();
            });
            services.AddSingleton<IMapper>(s => config.CreateMapper());
            return services;
        }
    }
}
