using AutoMapper;
using Kompass.Application.DTOs.Queries;

using Kompass.Domain.Interfaces.Projects;

namespace Kompass.Application.Common.Mappings;

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