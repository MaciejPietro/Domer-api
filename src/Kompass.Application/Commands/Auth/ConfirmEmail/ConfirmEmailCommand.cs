using MediatR;

namespace Kompass.Application.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<Unit>
{
    public string Email { get; init; }
    public string Token { get; init; }
}