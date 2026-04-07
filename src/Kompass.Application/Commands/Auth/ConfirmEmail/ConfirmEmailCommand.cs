using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<Result<Unit>>
{
    public string Email { get; init; }
    public string Token { get; init; }
}