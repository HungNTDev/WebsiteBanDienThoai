using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.ProductManagement.Commands.Update
{
    public sealed class UpdateProductCommandHandler
        : ICommandHandler<UpdateProductCommand, ApiResponse<object>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateProductDto> _validator;

        public UpdateProductCommandHandler(IProductRepository productRepository,
                                           IUnitOfWork unitOfWork,
                                           ILogger<UpdateProductCommandHandler> logger,
                                           IMapper mapper,
                                           IValidator<UpdateProductDto> validator)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<object>> Handle(UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = _validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.Result.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Result.Errors);
                }
                var product = await _productRepository.GetByIdAsync(model.Id);
                if (product == null)
                {
                    return ApiResponseBuilder.Error<object>("Product not found", statusCode: 404);
                }
                var isProductExists = await _productRepository
                    .IsProductExistsAsync(model.Name, cancellationToken);
                if (!isProductExists)
                {
                    return ApiResponseBuilder.Error<object>("Product already exists", statusCode: 409);
                }
                var productToUpdate = _mapper.Map(model, product);
                productToUpdate.UpdatedBy = request.userName;
                _productRepository.Update(productToUpdate);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>(productToUpdate,
                    "Product updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while create product");
                return ApiResponseBuilder.Error<object>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
