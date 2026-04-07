using Ardalis.Result;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Auth.RemindPassword;

public class RemindPasswordCommandHandler : IRequestHandler<RemindPasswordCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;


    public RemindPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Unit>> Handle(RemindPasswordCommand request, CancellationToken cancellationToken)
    {
        if (await _identityService.IsUserExists(request.Email) == false)
        {
            return Result<Unit>.Error("Użytkownik o podanym adresie nie istnieje.");
        }

        if (await _identityService.HasConfirmedEmail(request.Email) == false)
        {
            return Result<Unit>.Error("Użytkownik nie ma potwierdzonego adresu email.");
        }


        IApplicationUser user = await _identityService.GetUserDetailsByEmailAsync(request.Email);

        string token = await _identityService.GenerateRemindPasswordTokenAsync(user);

        await _identityService.SendResetPasswordEmail(request.ClientUri, user.Email, token);

        return Result<Unit>.Success(Unit.Value);
    }
}