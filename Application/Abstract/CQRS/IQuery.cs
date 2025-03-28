using MediatR;

namespace Application.Abstract.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
