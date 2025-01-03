using Domer.Application.Common.Interfaces;
using Domer.Application.DTOs.Queries;
using Domer.Domain.Interfaces;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Queries.User.GetCurrentUser;

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
        
        return new UserDto
        {
            Id = currentUser.Id,
            Email = currentUser.Email,
            IsEmailConfirmed = currentUser.EmailConfirmed,
        };
    }
}