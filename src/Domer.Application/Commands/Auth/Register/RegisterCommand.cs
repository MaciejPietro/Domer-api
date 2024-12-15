using MediatR;

namespace Domer.Application.Commands.Auth.Register;

public class RegisterCommand : IRequest<Unit>
{
  
    public string Email { get; set; }

    public string Password { get; set; }
    
    public string ClientUri { get; set; }
}