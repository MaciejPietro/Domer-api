using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Auth.Register;

public class RegisterCommand : IRequest<Result<Unit>>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string ClientUri { get; set; }
}