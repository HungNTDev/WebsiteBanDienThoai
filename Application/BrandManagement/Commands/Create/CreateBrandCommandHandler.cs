using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.BrandManagement.Commands.Create
{
    public class CreateBrandCommandHandler : ICommandHandler<CreateBrandCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateBrandCommandHandler> _logger;
        private readonly IValidator<CreateBrandDto> _validator;
        private readonly IBrandRepository _brandRepository;
        public CreateBrandCommandHandler(IUnitOfWork unitOfWork,
                                         ILogger<CreateBrandCommandHandler> logger,
                                         IValidator<CreateBrandDto> validator,
                                         IBrandRepository brandRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validator = validator;
            _brandRepository = brandRepository;
        }
        public async Task<ApiResponse<object>> Handle(CreateBrandCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = await _validator.ValidateAsync(model,
                cancellationToken);

                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(
                        validationResult.Errors);
                }
                var brand = new Domain.Entities.Brand
                {
                    Name = model.Name,
                    Image = model.Image,
                    CreatedBy = request.userName
                };
                await _unitOfWork.BrandRepository.CreateAsync(brand);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("",
                    $"Thêm mới thương hiệu sản phẩm thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating variation option ");
                throw;  // Re-throwing the exception after logging it.
            }
        }
    }
}
