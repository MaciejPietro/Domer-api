using Domer.Application.Commands.User.ResendEmailConfirmation;
using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.User.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public DeleteUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Password))
        {
            throw new BadRequestException("Password is required to delete account.");
        }
        
        IApplicationUser? user = await _identityService.GetUserDetailsAsync(request.Id);
        
        
        bool isCurrentPasswordCorrect = await _identityService.CheckPasswordAsync(user, request.Password);
    
        if (!isCurrentPasswordCorrect)
        {
            throw new BadRequestException("Błędne hasło.");
        }
        
        await _identityService.DeleteUserAsync(user);

        return Unit.Value;
    }
}