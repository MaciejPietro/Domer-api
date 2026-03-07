using MediatR;

namespace Kompass.Application.Commands.User.ResendEmailConfirmation;

public class ResendEmailConfirmationCommand : IRequest<Unit>
{
    public string Email { get; set;}
    public string ClientUri { get;set; }
}