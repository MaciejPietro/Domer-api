using AutoMapper;
using Kompass.Application.DTOs;
using Kompass.Application.DTOs.Queries;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Projects;

namespace Kompass.Application.Common.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {

        CreateMap<IApplicationUser, AuthResponseDTO>().ForMember(dest => dest.EmailConfirmed, 
            opt => opt.MapFrom(src => src.EmailConfirmed));
        
      
    }
}

