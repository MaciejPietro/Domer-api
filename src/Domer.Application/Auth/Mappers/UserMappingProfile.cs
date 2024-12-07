using AutoMapper;
using Domer.Application.Auth.DTOs;
using Domer.Domain.Auth.Interfaces;

namespace Domer.Application.Auth.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {

        CreateMap<IApplicationUser, UserDto>().ForMember(dest => dest.IsEmailConfirmed, 
            opt => opt.MapFrom(src => src.EmailConfirmed));
    }
}

