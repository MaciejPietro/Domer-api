using MediatR;
using System;

namespace Domer.Application.Commands.User.UpdateUser;

public class UpdateUserCommand : IRequest<Unit>
{

 
    public string Id { get; set; }

    public string? Email { get; set;}

    public string? Password { get; set; }
    
    public string? CurrentPassword { get; set;}
    public string? ClientUri { get;set; }

}