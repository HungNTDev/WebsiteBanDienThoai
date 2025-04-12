using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.VariationOptionManagement.Queries.GetById;
using OneOf;

namespace Application.VariationOptionManagement.Queries.GetById
{
    public record GetVariationOptionByIdQuery(Guid Id)
        : IQuery<OneOf<ApiResponse<GetVariationOptionByIdDto>, GetVariationOptionByIdDto>>;
}
