using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.BrandManagement.Commands.Update
{
    public class UpdateBrandCommandHandler : ICommandHandler<UpdateBrandCommand, ApiResponse<object>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateBrandDto> _validator;
        private readonly ILogger<UpdateBrandCommandHandler> _logger;
        private readonly IMapper _mapper;
        public UpdateBrandCommandHandler(IMapper mapper,
            ILogger<UpdateBrandCommandHandler> logger,
            IValidator<UpdateBrandDto> validator,
            IUnitOfWork unitOfWork, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
        }
        public async Task<ApiResponse<object>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = _validator.Validate(model);
                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }
                var brand = await _brandRepository.GetByIdAsync(model.Id);
                if (brand == null)
                {
                    return ApiResponseBuilder.Error<object>("Không tìm thấy thương hiệu", statusCode: 404);
                }
                var updateBrand = _mapper.Map(model, brand);
                updateBrand.UpdatedBy = request.userName;
                _brandRepository.Update(brand);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", $"Cập nhật thương hiệu sản phẩm  thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }
    }
}
