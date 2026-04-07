using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Auth.ResendConfirmationEmail;

public class ResendConfirmationEmailCommand : IRequest<Result<Unit>>
{
    public string Email { get; set; }
    
    public string ClientUri { get; set; }
}