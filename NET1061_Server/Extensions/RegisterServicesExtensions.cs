using Application.Marker;
using System.Reflection;

namespace NET1061_Server.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly
            (typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));
            return services;
        }
    }
}
