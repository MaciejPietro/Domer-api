using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Users;
using MediatR;

namespace Kompass.Application.Queries.User.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<UserDto>
{
    public System.Security.Claims.ClaimsPrincipal User { get; init; }

    public GetCurrentUserQuery(System.Security.Claims.ClaimsPrincipal user)
    {
        User = user;
    }
}