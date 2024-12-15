using AutoMapper;
using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Application.DTOs;
using Domer.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public ConfirmEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        if (await _identityService.IsUserExists(request.Email) == false)
        {
            throw new BadRequestException("Użytkownik o takim adresie email nie istnieje");
        }
        
        await _identityService.ConfirmUserEmail(request.Email, request.Token);
        
        return Unit.Value;
    }
}