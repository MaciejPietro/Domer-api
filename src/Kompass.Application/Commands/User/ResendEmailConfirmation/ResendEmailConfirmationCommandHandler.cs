using Ardalis.Result;
using Kompass.Application.Commands.User.UpdateUser;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Users;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Kompass.Application.Commands.User.ResendEmailConfirmation;

public class ResendEmailConfirmationCommandHandler : IRequestHandler<ResendEmailConfirmationCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;


    public ResendEmailConfirmationCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Unit>> Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        IApplicationUser? user = await _identityService.GetUserDetailsByEmailAsync(request.Email);

        try
        {
            // string token = await _identityService.GenerateEmailConfirmationTokenAsync(user);
            //
            // await _identityService.SendConfirmationEmail(request.ClientUri, user.Email, token);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Error("Coś poszło nie tak!");
        }

        return Result<Unit>.Success(Unit.Value);
    }
}