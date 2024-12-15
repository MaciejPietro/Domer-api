using AutoMapper;
using Domer.Application.Commands.Auth.ResendConfirmationEmail;
using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Application.DTOs;
using Domer.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.Auth.ResendConfirmationEmail;

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
        
        IApplicationUser user = await _identityService.GetUserDetailsAsync(request.Email);
        
        string token = await _identityService.GenerateEmailConfirmationTokenAsync(user);
        
        await _identityService.SendConfirmationEmail(request.ClientUri, user.Email, token);
    
        return Unit.Value;

    }
}