using Application.Abstract;
using Application.Abstract.CQRS;
using Application.VariationManagement.Queries.GetAll;

namespace Application.VariationOptionManagement.Queries.GetAll
{
    public record GetAllVariationOptionQuery(Filter Filter) :
        IQuery<ApiResponse<PaginatedResult<GetAllVariationOptionDto>>>;
}
