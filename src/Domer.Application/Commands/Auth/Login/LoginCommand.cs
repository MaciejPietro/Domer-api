
using Domer.Application.DTOs;
using MediatR;


namespace Domer.Application.Commands.Auth.Login;
    public class LoginCommand : IRequest<AuthResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


   