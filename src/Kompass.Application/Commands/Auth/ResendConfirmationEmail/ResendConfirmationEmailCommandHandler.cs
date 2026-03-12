using AutoMapper;
using Kompass.Application.Commands.Auth.ResendConfirmationEmail;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Application.DTOs;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Auth.ResendConfirmationEmail;

public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public ResendConfirmationEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
   
        if (await _identityService.HasConfirmedEmail(request.Email) == false)
        {
            throw new BadRequestException("Użytkownik o podanym adresie nie ma potwierdzonego adresu email.");
        }
        
        IApplicationUser user = await _identityService.GetUserDetailsByEmailAsync(request.Email);
        
        string token = await _identityService.GenerateEmailConfirmationTokenAsync(user);
        
        await _identityService.SendConfirmationEmail(request.ClientUri, user.Email, token);
    
        return Unit.Value;

    }
}