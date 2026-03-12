using AutoMapper;
using Kompass.Application.DTOs;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Users;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Projects;
using Kompass.Domain.Interfaces.Users;

namespace Kompass.Application.Common.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {

        CreateMap<IApplicationUser, AuthResponseDTO>().ForMember(dest => dest.EmailConfirmed,
            opt => opt.MapFrom(src => src.EmailConfirmed));

        CreateMap<IApplicationUser, UserListDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));

    }
}

