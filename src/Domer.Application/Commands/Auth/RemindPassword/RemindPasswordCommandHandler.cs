using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.Auth.RemindPassword;

public class RemindPasswordCommandHandler : IRequestHandler<RemindPasswordCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public RemindPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(RemindPasswordCommand request, CancellationToken cancellationToken)
    {
        if (await _identityService.IsUserExists(request.Email) == false)
        {
            throw new BadRequestException("Użytkownik o podanym adresie nie istnieje.");
        }

        
        if (await _identityService.HasConfirmedEmail(request.Email) == false)
        {
            throw new BadRequestException("Użytkownik nie ma potwierdzonego adresu email.");
        }
        
        
        IApplicationUser user = await _identityService.GetUserDetailsByEmailAsync(request.Email);
        
        string token = await _identityService.GenerateRemindPasswordTokenAsync(user);
        
        await _identityService.SendResetPasswordEmail(request.ClientUri, user.Email, token);
    
        return Unit.Value;

    }
}