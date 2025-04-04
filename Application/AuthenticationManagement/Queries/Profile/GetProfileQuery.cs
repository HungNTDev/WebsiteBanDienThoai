using Application.Abstract;
using Application.Abstract.CQRS;
using OneOf;

namespace Application.AuthenticationManagement.Queries.Profile
{
    public record GetProfileQuery(Guid Id) : IQuery<OneOf<ApiResponse<GetProfileDto>, GetProfileDto>>;
}
