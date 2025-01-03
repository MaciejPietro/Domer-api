using Domer.Application.Commands.User.UpdateUser;
using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Domer.Application.Commands.User.ResendEmailConfirmation;

public class ResendEmailConfirmationCommandHandler : IRequestHandler<ResendEmailConfirmationCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public ResendEmailConfirmationCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        IApplicationUser? user = await _identityService.GetUserDetailsByEmailAsync(request.Email);
        
        
        try
        {
            string token = await _identityService.GenerateEmailConfirmationTokenAsync(user);
                
            await _identityService.SendConfirmationEmail(request.ClientUri, user.Email, token);
        }
        catch (Exception ex)
        {
            throw new InternalException("Coś poszło nie tak!", ex);
        }
        
        return Unit.Value;
    }
}