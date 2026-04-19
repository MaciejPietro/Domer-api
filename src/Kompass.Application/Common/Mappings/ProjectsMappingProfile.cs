using AutoMapper;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Projects;
using Kompass.Domain.Interfaces.Projects;

namespace Kompass.Application.Common.Mappings;

public class ProjectsMappingProfile : Profile
{
    public ProjectsMappingProfile()
    {
        CreateMap<IProject, ProjectDto>()
            .ForMember(dest => dest.Details, opt =>
                opt.MapFrom(src => src.ProjectDetails));
         
        
        CreateMap<IProjectDetails, ProjectDetailsDto>();
    }
}