using Ardalis.Result;
using Kompass.Application.Commands.User.ResendEmailConfirmation;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Users;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.User.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;


    public DeleteUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Password))
        {
            return Result<Unit>.Error("Password is required to delete account.");
        }

        IApplicationUser? user = await _identityService.GetUserDetailsAsync(request.Id);

        bool isCurrentPasswordCorrect = await _identityService.CheckPasswordAsync(user, request.Password);

        if (!isCurrentPasswordCorrect)
        {
            return Result<Unit>.Error("Błędne hasło.");
        }

        await _identityService.DeleteUserAsync(user);

        return Result<Unit>.Success(Unit.Value);
    }
}