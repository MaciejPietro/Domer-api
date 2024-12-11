using AutoMapper;
using Domer.Application.Commands.Auth.Logout;
using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Application.DTOs;
using Domer.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.Auth.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public LogoutCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _identityService.LogoutUserAsync();
        return Unit.Value;
    }
}