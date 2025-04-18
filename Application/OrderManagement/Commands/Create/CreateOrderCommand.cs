using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.OrderManagement.Commands.Create
{
    public record CreateOrderCommand(CreateOrderDto model) : ICommand<ApiResponse<object>>;
}
