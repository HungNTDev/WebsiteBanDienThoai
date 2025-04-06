using Application.Abstract;
using Application.Abstract.CQRS;

namespace Application.ProductManagement.Queries.GetDetail
{
    public record GetDetailProductQuery(Guid Id)
       : IQuery<ApiResponse<GetDetailProductDto>>;
}
