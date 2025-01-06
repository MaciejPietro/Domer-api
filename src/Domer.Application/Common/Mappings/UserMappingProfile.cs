using AutoMapper;
using Domer.Application.DTOs;
using Domer.Application.DTOs.Queries;
using Domer.Domain.Entities.Projects;
using Domer.Domain.Interfaces;
using Domer.Domain.Interfaces.Projects;

namespace Domer.Application.Common.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {

        CreateMap<IApplicationUser, AuthResponseDTO>().ForMember(dest => dest.IsEmailConfirmed, 
            opt => opt.MapFrom(src => src.EmailConfirmed));
        
      
    }
}

