using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.SeriesManagement.Commands.Update
{
    public class UpdateSeriesCommandHandler
        : ICommandHandler<UpdateSeriesCommand, ApiResponse<object>>
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateSeriesDto> _validator;
        private readonly ILogger<UpdateSeriesCommandHandler> _logger;

        public UpdateSeriesCommandHandler(ILogger<UpdateSeriesCommandHandler> logger,
            ISeriesRepository seriesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IValidator<UpdateSeriesDto> validator)
        {
            _logger = logger;
            _seriesRepository = seriesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ApiResponse<object>> Handle(UpdateSeriesCommand request,
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
                var series = await _seriesRepository.GetByIdAsync(model.Id);
                if (series == null)
                {
                    return ApiResponseBuilder.Error<object>("Không tìm thấy series", statusCode: 404);
                }
                var updateSeries = _mapper.Map(model, series);
                updateSeries.UpdatedBy = request.userName;
                _seriesRepository.Update(series);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", $"Cập nhật series sản phẩm thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }
    }
}
