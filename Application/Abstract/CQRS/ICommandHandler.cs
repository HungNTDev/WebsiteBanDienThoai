﻿using MediatR;

namespace Application.Abstract.CQRS
{
    public interface ICommandHandler<in TCommand, TResponse>: IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {  }
}
