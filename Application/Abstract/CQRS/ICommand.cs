using MediatR;

namespace Application.Abstract.CQRS
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {  }
}
