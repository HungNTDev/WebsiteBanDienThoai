﻿using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.ProductManagement.Commands.Create
{
    public record CreateProductCommand(CreateProductDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
