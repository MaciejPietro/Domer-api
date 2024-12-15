using AutoMapper;
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

namespace Domer.Application.Commands.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Email);
        if (await _identityService.IsUserExists(request.Email))
        {
            throw new BadRequestException("Użytkownik o takim adresie email już istnieje");
        }
        
        IApplicationUser user = await _identityService.CreateUserAsync(request.Email, request.Password);
        
        
        string token = await _identityService.GenerateEmailConfirmationTokenAsync(user);
        
        await _identityService.SendConfirmationEmail(request.ClientUri, user.Email, token);
    
        return Unit.Value;
    }
}