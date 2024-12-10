using AutoMapper;
using Domer.Application.DTOs.Queries;
using Domer.Domain.Interfaces;

namespace Domer.Application.Common.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {

        CreateMap<IApplicationUser, UserDto>().ForMember(dest => dest.IsEmailConfirmed, 
            opt => opt.MapFrom(src => src.EmailConfirmed));
    }
}

