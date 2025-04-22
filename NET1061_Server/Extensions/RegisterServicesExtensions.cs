using Application.Abstract.Marker;
using Application.Abstract.Services;
using System.Reflection;

namespace NET1061_Server.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly
            (typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddHttpClient<IPayPalService, PayPalService>();
            return services;
        }
    }
}
