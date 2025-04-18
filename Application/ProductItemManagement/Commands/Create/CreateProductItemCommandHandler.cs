using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.ProductItemManagement.Commands.Create
{
    public class CreateProductItemCommandHandler : ICommandHandler<CreateProductItemCommand, ApiResponse<object>>
    {
        private readonly ILogger<CreateProductItemCommandHandler> _logger;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductItemDto> _validator;
        private readonly IMapper _mapper;
        public CreateProductItemCommandHandler(
            ILogger<CreateProductItemCommandHandler> logger,
            IProductItemRepository productItemRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateProductItemDto> validator,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _logger = logger;
            _productItemRepository = productItemRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> Handle(CreateProductItemCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.Model;
            try
            {
                var validationResult = await _validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var productItemExisted = await _productItemRepository.IsProductItemExistAsync(model.SKU!,
                    cancellationToken);
                if (productItemExisted)
                {
                    return ApiResponseBuilder.Error<object>($"Sản phẩm với mã {model.SKU} đã tồn tại!");
                }

                var isProductExist = _productRepository.GetByIdAsync(model.ProductId);
                if (isProductExist is null)
                {
                    return ApiResponseBuilder.Error<object>("Sản phẩm này không tồn tại!");
                }
                var productItem = new ProductItem
                {
                    SKU = model.SKU,
                    Name = model.Name,
                    Price = model.Price,
                    ProductId = model.ProductId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = request.userName,
                    Image = model.Image,
                };
                var optionIds = model.Options.Select(o => o.OptionId ?? Guid.Empty).ToList();
                await _productItemRepository.CreateAsync(productItem, optionIds);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponseBuilder.Success<object>("", $"Thêm sản phẩm {model.SKU} thành công",
                    statusCode: 201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Create Product Item");
                return ApiResponseBuilder.Error<object>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
