using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.User.ResendEmailConfirmation;

public class ResendEmailConfirmationCommand : IRequest<Result<Unit>>
{
    public string Email { get; set;}
    public string ClientUri { get;set; }
}