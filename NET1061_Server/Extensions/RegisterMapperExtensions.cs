﻿using Application.MapperProfile;
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
            });
            services.AddSingleton<IMapper>(s => config.CreateMapper());
            return services;
        }
    }
}
