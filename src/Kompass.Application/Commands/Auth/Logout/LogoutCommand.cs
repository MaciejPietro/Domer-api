using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Auth.Logout;

public class LogoutCommand : IRequest<Result<Unit>>
{
}  