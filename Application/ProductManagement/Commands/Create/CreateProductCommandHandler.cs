using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.ProductManagement.Commands.Create
{
    public sealed class CreateProductCommandHandler
        : ICommandHandler<CreateProductCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductDto> _validator;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger,
                                           IValidator<CreateProductDto> validator,
                                           IProductRepository productRepository,
                                           IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _validator = validator;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<object>> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = await _validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }
                var isProductExists = await _productRepository
                    .IsProductExistsAsync(model.Name, cancellationToken);
                if (isProductExists)
                {
                    return ApiResponseBuilder.Error<object>("Product already exists", statusCode: 409);
                }
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Image = model.Image,
                    CreatedDate = DateTime.UtcNow,
                    SeriesId = model.SeriesId,
                    CategoryId = model.CategoryId,
                    CreatedBy = request.userName,
                };
                await _productRepository.CreateAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>(product.Id, "Product created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while create product");
                return ApiResponseBuilder.Error<object>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
