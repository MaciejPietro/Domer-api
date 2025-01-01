using AutoMapper;
using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Application.DTOs;
using Domer.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Commands.Auth.Login;

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
        if (!result) throw new NotFoundException("Błędne hasło");

        IApplicationUser user = await _identityService.GetUserDetailsAsync(request.Email);

        return _mapper.Map<AuthResponseDTO>(user);
    }
}