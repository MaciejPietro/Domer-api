using AutoMapper;
using Kompass.Application.Common.Exceptions;
using Kompass.Application.Common.Interfaces;
using Kompass.Application.DTOs;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDTO>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    
    public LoginCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }
    
    public async Task<AuthResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        bool isUserExists = await _identityService.IsUserExists(request.Email!);
        if (!isUserExists) throw new NotFoundException("Nie znaleźliśmy użytkownika o takim adresie email.");  
        
        
        bool result = await _identityService.SigninUserAsync(request.Email, request.Password);
        if (!result) throw new BadRequestException("Dane logowania są nieprawidłowe");

        IApplicationUser user = await _identityService.GetUserDetailsByEmailAsync(request.Email);

        var response = _mapper.Map<AuthResponseDTO>(user);
        response.Roles = await _identityService.GetUserRolesAsync(user.Id.ToString());
        return response;
    }
}