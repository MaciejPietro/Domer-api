using Ardalis.Result;
using AutoMapper;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Application.DTOs;
using Kompass.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Auth.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;


    public ResetPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        bool user = await _identityService.IsUserExists(request.Email);

        if (user == false)
            return Result<Unit>.Error("Użytkownik nie istnieje");

        string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

        IdentityResult resetPassResult = await _identityService.ResetPasswordAsync(request.Email, decodedToken, request.Password);
        if (!resetPassResult.Succeeded)
        {
            var errorMessage = resetPassResult.Errors.Any(e =>
                e.Code.Contains("InvalidToken", StringComparison.OrdinalIgnoreCase))
                ? "Nieprawidłowy token"
                : "Coś poszło nie tak";
            return Result<Unit>.Error(errorMessage);
        }

        return Result<Unit>.Success(Unit.Value);
    }
}