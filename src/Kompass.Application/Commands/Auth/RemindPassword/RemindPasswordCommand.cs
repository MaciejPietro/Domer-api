using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Auth.RemindPassword;

public class RemindPasswordCommand : IRequest<Result<Unit>>
{
    public string Email { get; set; }
    
    public string ClientUri { get; set; }
}