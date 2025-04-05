using Application.AuthenticationManagement.Commands.ForgotPassword;
using Application.AuthenticationManagement.Commands.Login;
using Application.AuthenticationManagement.Commands.Register;
using Application.AuthenticationManagement.Commands.ResetPassword;
using Application.CategoryManagement.Commands.Create;
using Application.CategoryManagement.Commands.Update;
using Application.VariationManagement.Commands.Create;
using Application.VariationManagement.Commands.Update;
using Application.VariationOptionManagement.Commands.Create;
using Application.VariationOptionManagement.Commands.Update;
using FluentValidation;

namespace NET1061_Server.Extensions
{
    public static class RegisterFluentValidationExtensions
    {
        public static IServiceCollection
            AddRegistrationFluentValidations(this IServiceCollection services)
        {
            // Authentication
            services.AddScoped<IValidator<RegisterModel>, RegisterModelValidator>();
            services.AddScoped<IValidator<LoginModel>, LoginModelValidator>();
            services.AddScoped<IValidator<ForgotPasswordModel>, ForgotPasswordValidator>();
            services.AddScoped<IValidator<ResetPasswordModels>, ResetPasswordModelValidator>();

            // Category
            services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidation>();
            services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryValidation>();

            //Variation
            services.AddScoped<IValidator<CreateVariationDto>, CreateVariationValidation>();
            services.AddScoped<IValidator<UpdateVariationDto>, UpdateVariationValidation>();

            //Variation Option
            services.AddScoped<IValidator<CreateVariationOptionDto>, CreateVariationOptionValidation>();
            services.AddScoped<IValidator<UpdateVariationOptionDto>, UdpateVariationOptionValidation>();

            return services;
        }
    }
}
