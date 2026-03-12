using Kompass.Application.Common.Interfaces;
using Kompass.Application.DTOs.Queries.Users;
using Kompass.Application.Queries.User.GetCurrentUser;
using Kompass.Domain.Interfaces.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Queries.Users.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    private readonly IIdentityService _identityService;
    
    public GetCurrentUserQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        IApplicationUser? currentUser = await _identityService.GetUserAsync(request.User);
        
        var roles = await _identityService.GetUserRolesAsync(currentUser.Id.ToString());

        return new UserDto
        {
            Id = currentUser.Id,
            Email = currentUser.Email,
            EmailConfirmed = currentUser.EmailConfirmed,
            Roles = roles,
        };
    }
}