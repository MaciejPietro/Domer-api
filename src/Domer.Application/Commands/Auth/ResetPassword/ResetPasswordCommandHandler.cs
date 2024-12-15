using AutoMapper;
using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Application.DTOs;
using Domer.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.Auth.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly IIdentityService _identityService;

    
    public ResetPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        bool user = await _identityService.IsUserExists(request.Email);
        
        if (user == false) throw new BadRequestException("Użytkownik nie istnieje");
        
        string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
        
        IdentityResult resetPassResult = await _identityService.ResetPasswordAsync(request.Email, decodedToken, request.Password);
        if (!resetPassResult.Succeeded)
        {
            return resetPassResult.Errors.Any(e => 
                e.Code.Contains("InvalidToken", StringComparison.OrdinalIgnoreCase))
                ? throw new BadRequestException("Nieprawidłowy token")
                : throw new BadRequestException("Coś poszło nie tak");
        }
        
        return Unit.Value;
    }
}