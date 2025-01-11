using AutoMapper;
using Domer.Application.DTOs.Queries;

using Domer.Domain.Interfaces.Projects;

namespace Domer.Application.Common.Mappings;

public class ProjectsMappingProfile : Profile
{
    public ProjectsMappingProfile()
    {
        CreateMap<IProject, ProjectDto>()
            .ForMember(dest => dest.Details, opt =>
                opt.MapFrom(src => src.ProjectDetails));
            // .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<IProjectDetails, ProjectDetailsDto>();
    }
}