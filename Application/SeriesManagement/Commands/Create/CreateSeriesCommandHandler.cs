using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.SeriesManagement.Commands.Create
{
    public class CreateSeriesCommandHandler : ICommandHandler<CreateSeriesCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSeriesCommandHandler> _logger;
        private readonly IValidator<CreateSeriesDto> _validator;
        private readonly ISeriesRepository _seriesRepository;

        public CreateSeriesCommandHandler(ISeriesRepository seriesRepository,
                                          IValidator<CreateSeriesDto> validator,
                                          ILogger<CreateSeriesCommandHandler> logger,
                                          IUnitOfWork unitOfWork)
        {
            _seriesRepository = seriesRepository;
            _validator = validator;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<object>> Handle(CreateSeriesCommand request,
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
                var series = new Domain.Entities.Series
                {
                    Name = model.Name,
                    CreatedBy = request.userName
                };
                await _unitOfWork.SeriesRepository.CreateAsync(series);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", $"Thêm mới bộ sưu tập sản phẩm thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating");
                throw;  // Re-throwing the exception after logging it.
            }
        }
    }
}
