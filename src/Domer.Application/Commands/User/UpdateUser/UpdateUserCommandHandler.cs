using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.User.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public UpdateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        IApplicationUser? user = await _identityService.GetUserDetailsAsync(request.Id);

        if (user is null)
            throw new BadRequestException("Użytkownik nie istnieje");
        
        if(!string.IsNullOrEmpty(request.Email))
        {
            if (string.IsNullOrEmpty(request.ClientUri))
                throw new BadRequestException("Client URI jest wymagane");
            
            if (request.Email is not null && await _identityService.IsUserExists(request.Email))
                throw new BadRequestException("Użytkownik o takim adresie email już istnieje");
            
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
                throw new BadRequestException("Obecne hasło jest wymagane jeśli chcesz zmienić hasło.");
            
            await _identityService.UpdateUserPassword(request.Id, request.Password, request.CurrentPassword);
        }
        
         
   

        return Unit.Value;
    }
}