﻿using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.ProductManagement.Queries.GetAll
{
    public record GetAllProductQuery(Filter filter) : IQuery<ApiResponse<PaginatedResult<GetAllProductDto>>>;
}
