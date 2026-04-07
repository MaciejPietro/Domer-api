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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;


    public ConfirmEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Unit>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        if (await _identityService.IsUserExists(request.Email) == false)
        {
            return Result<Unit>.Error("Użytkownik o takim adresie email nie istnieje");
        }

        await _identityService.ConfirmUserEmail(request.Email, request.Token);

        return Result<Unit>.Success(Unit.Value);
    }
}