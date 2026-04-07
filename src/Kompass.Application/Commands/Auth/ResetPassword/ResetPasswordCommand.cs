using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Auth.ResetPassword;

public class ResetPasswordCommand : IRequest<Result<Unit>>
{
    public string Email { get; set; }
    
    public string Token { get; set; }
    
    public string Password { get; set; }
}