using AutoMapper;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Application.DTOs;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Enums.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _identityService.IsUserExists(request.Email))
        {
            throw new BadRequestException("Użytkownik o takim adresie e-mail już istnieje");
        }
        
        IApplicationUser user = await _identityService.CreateUserAsync(request.Email, request.Password);

        await _identityService.AssignUserToRoleAsync(user.Id.ToString(), nameof(UserRole.Viewer));

        // Temporary disabled
        // string token = await _identityService.GenerateEmailConfirmationTokenAsync(user);
        // await _identityService.SendConfirmationEmail(request.ClientUri, user.Email, token);

        return Unit.Value;
    }
}