using Ardalis.Result;
using MediatR;
using System;

namespace Kompass.Application.Commands.User.UpdateUser;

public class UpdateUserCommand : IRequest<Result<Unit>>
{
    public string Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? CurrentPassword { get; set; }
    public string? ClientUri { get; set; }
}