using Ardalis.Result;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Users;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.User.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;


    public UpdateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        IApplicationUser? user = await _identityService.GetUserDetailsAsync(request.Id);

        if (user is null)
            return Result<Unit>.Error("Użytkownik nie istnieje");

        if (!string.IsNullOrEmpty(request.Email))
        {
            if (string.IsNullOrEmpty(request.ClientUri))
                return Result<Unit>.Error("Client URI jest wymagane");

            if (request.Email is not null && await _identityService.IsUserExists(request.Email))
                return Result<Unit>.Error("Użytkownik o takim adresie email już istnieje");

            // CHANGE EMAIL
            if (request.Email != user.Email)
            {
                await _identityService.UpdateUserProfile(user, request.Email!);

                string token = await _identityService.GenerateEmailConfirmationTokenAsync(user);

                await _identityService.SendConfirmationEmail(request.ClientUri, user.Email, token);
            }
        }

        // CHANGE PASSWORD
        if (!string.IsNullOrEmpty(request.Password))
        {
            if (string.IsNullOrEmpty(request.CurrentPassword))
                return Result<Unit>.Error("Obecne hasło jest wymagane jeśli chcesz zmienić hasło.");

            await _identityService.UpdateUserPassword(request.Id, request.Password, request.CurrentPassword);
        }

        return Result<Unit>.Success(Unit.Value);
    }
}