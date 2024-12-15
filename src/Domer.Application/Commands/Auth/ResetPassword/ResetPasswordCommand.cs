using MediatR;

namespace Domer.Application.Commands.Auth.ResetPassword;

public class ResetPasswordCommand : IRequest<Unit>
{
    public string Email { get; set; }
    
    public string Token { get; set; }
    
    public string Password { get; set; }
}