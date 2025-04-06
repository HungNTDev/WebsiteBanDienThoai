using Application.Abstract;
using Application.Abstract.CQRS;

namespace Application.SeriesManagement.Commands.Create
{
    public record CreateSeriesCommand(CreateSeriesDto model, string userName) : ICommand<ApiResponse<object>>;
}
