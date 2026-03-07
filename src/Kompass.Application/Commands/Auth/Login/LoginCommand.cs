
using Kompass.Application.DTOs;
using MediatR;


namespace Kompass.Application.Commands.Auth.Login;
// TODO command should not return anything
    public class LoginCommand : IRequest<AuthResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


   