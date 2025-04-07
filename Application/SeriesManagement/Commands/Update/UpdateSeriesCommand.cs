using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.SeriesManagement.Commands.Update
{
    public record UpdateSeriesCommand(UpdateSeriesDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
