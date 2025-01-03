using MediatR;

namespace Domer.Application.Commands.User.DeleteUser;

public class DeleteUserCommand  : IRequest<Unit>
{
    public string Id { get;set; }
    public string Password { get; set;}
}