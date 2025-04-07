using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using OneOf;

namespace Application.BrandManagement.Queries.GetDetail
{
    public record GetDetailBrandQuery(Guid Id)
        : IQuery<OneOf<ApiResponse<GetDetailBrandDto>, GetDetailBrandDto>>;
}
