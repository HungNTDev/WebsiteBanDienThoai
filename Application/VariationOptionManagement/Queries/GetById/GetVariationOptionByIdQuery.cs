using Application.Abstract;
using Application.Abstract.CQRS;
using Application.VariationManagement.Queries.GetById;
using OneOf;

namespace Application.VariationOptionManagement.Queries.GetById
{
    public record GetVariationOptionByIdQuery(Guid Id)
        : IQuery<OneOf<ApiResponse<GetVariationOptionDto>, GetVariationOptionDto>>;
}
