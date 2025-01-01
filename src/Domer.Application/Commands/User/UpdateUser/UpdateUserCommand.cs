using MediatR;
using System;

namespace Domer.Application.Commands.User.UpdateUser;

public class UpdateUserCommand : IRequest<Unit>
{

 
    public string Id { get; internal set; }

    public string? Email { get; }

    public string? Password { get; }
    
    public string? CurrentPassword { get; }
    public string? ClientUri { get; }

}