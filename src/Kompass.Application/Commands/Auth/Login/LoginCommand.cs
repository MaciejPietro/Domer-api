using Ardalis.Result;
using Kompass.Application.DTOs;
using MediatR;


namespace Kompass.Application.Commands.Auth.Login;

public class LoginCommand : IRequest<Result<AuthResponseDTO>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}