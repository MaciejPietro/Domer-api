using MediatR;

namespace Kompass.Application.Commands.User.DeleteUser;

public class DeleteUserCommand  : IRequest<Unit>
{
    public string Id { get;set; }
    public string Password { get; set;}
}