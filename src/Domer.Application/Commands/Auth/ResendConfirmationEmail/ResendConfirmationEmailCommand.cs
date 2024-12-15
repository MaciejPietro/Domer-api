using MediatR;

namespace Domer.Application.Commands.Auth.ResendConfirmationEmail;

public class ResendConfirmationEmailCommand : IRequest<Unit>
{
    public string Email { get; set; }
    
    public string ClientUri { get; set; }
}