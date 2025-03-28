﻿using Application.Abstract;
using Application.Abstract.CQRS;

namespace Application.VariationManagement.Queries.GetAll
{
    public record GetAllVariationQuery(Filter Filter)
        : IQuery<ApiResponse<PaginatedResult<GetAllVariationDto>>>;
}
