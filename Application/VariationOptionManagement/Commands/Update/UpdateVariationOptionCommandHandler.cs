using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VariationOptionManagement.Commands.Update
{
    public class UpdateVariationOptionCommandHandler :
        ICommandHandler<UpdateVariationOptionCommand, ApiResponse<object>>
    {
        private readonly IVariationOptionRepository _variationOptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateVariationOptionDto> _validator;
        private readonly ILogger<UpdateVariationOptionCommandHandler> _logger;

        public UpdateVariationOptionCommandHandler(IVariationOptionRepository variationOptionRepository,
                                                   IUnitOfWork unitOfWork,
                                                   IMapper mapper,
                                                   IValidator<UpdateVariationOptionDto> validator,
                                                   ILogger<UpdateVariationOptionCommandHandler> logger)
        {
            _variationOptionRepository = variationOptionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<ApiResponse<object>> Handle(UpdateVariationOptionCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = _validator.Validate(model);
                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var variationOption = await
                    _variationOptionRepository.GetByIdAsync(model.Id);
                if (variationOption == null)
                {
                    return ApiResponseBuilder.Error<object>("Không tìm thấy biến thể", statusCode: 404);
                }

                var updateVariationOption = _mapper.Map(model, variationOption);
                updateVariationOption.UpdatedBy = request.userName;
                _variationOptionRepository.Update(updateVariationOption);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", $"Cập nhật biến thể sản phẩm  thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }
    }
}
