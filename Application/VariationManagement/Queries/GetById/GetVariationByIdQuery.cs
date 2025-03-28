using Application.Abstract;
using Application.Abstract.CQRS;
using OneOf;

namespace Application.VariationManagement.Queries.GetById
{
    public record class GetVariationByIdQuery(Guid Id)
        : IQuery<OneOf<ApiResponse<GetVariationDto>, GetVariationDto>>;
}
