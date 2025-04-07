using Application.AuthenticationManagement.Commands.ForgotPassword;
using Application.AuthenticationManagement.Commands.Login;
using Application.AuthenticationManagement.Commands.Register;
using Application.AuthenticationManagement.Commands.ResetPassword;
using Application.BrandManagement.Commands.Create;
using Application.BrandManagement.Commands.Update;
using Application.CategoryManagement.Commands.Create;
using Application.CategoryManagement.Commands.Update;
using Application.InventoryManagement.Commands.Create;
using Application.InventoryManagement.Commands.Update;
using Application.ProductItemManagement.Commands.Create;
using Application.ProductManagement.Commands.Create;
using Application.ProductManagement.Commands.Update;
using Application.SeriesManagement.Commands.Create;
using Application.SeriesManagement.Commands.Update;
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

            //Brand
            services.AddScoped<IValidator<CreateBrandDto>, CreateBrandValidation>();
            services.AddScoped<IValidator<UpdateBrandDto>, UpdateBrandValidation>();

            // Category
            services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidation>();
            services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryValidation>();

            //Variation
            services.AddScoped<IValidator<CreateVariationDto>, CreateVariationValidation>();
            services.AddScoped<IValidator<UpdateVariationDto>, UpdateVariationValidation>();

            //Variation Option
            services.AddScoped<IValidator<CreateVariationOptionDto>, CreateVariationOptionValidation>();
            services.AddScoped<IValidator<UpdateVariationOptionDto>, UdpateVariationOptionValidation>();

            //Product
            services.AddScoped<IValidator<CreateProductDto>, CreateProductValidation>();
            services.AddScoped<IValidator<UpdateProductDto>, UpdateProductValidation>();

            //Series
            services.AddScoped<IValidator<CreateSeriesDto>, CreateSeriesValidation>();
            services.AddScoped<IValidator<UpdateSeriesDto>, UpdateSeriesValidation>();

            //Inventory
            services.AddScoped<IValidator<CreateInventoryDto>, CreateInventoryValidation>();
            services.AddScoped<IValidator<UpdateInventoryDto>, UpdateInventoryValidation>();

            //Product Item
            services.AddScoped<IValidator<CreateProductItemDto>, CreateProductItemValidation>();

            return services;
        }
    }
}
