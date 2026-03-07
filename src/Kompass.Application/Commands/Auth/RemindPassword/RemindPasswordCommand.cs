using MediatR;
namespace Kompass.Application.Commands.Auth.RemindPassword;

public class RemindPasswordCommand  : IRequest<Unit>
{
    public string Email { get; set; }
    
    public string ClientUri { get; set; }
}