using Domer.Application.DTOs.Queries;
using MediatR;

namespace Domer.Application.Queries.User.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<UserDto>
{
    public System.Security.Claims.ClaimsPrincipal User { get; init; }

    public GetCurrentUserQuery(System.Security.Claims.ClaimsPrincipal user)
    {
        User = user;
    }
}