using AutoMapper;
using Kompass.Application.Commands.Auth.Logout;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Application.DTOs;
using Kompass.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Auth.Logout;

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