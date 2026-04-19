using AutoMapper;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Projects;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces.Projects;

namespace Kompass.Application.Common.Mappings;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<IProjectDetails, ProjectDetailsDto>(); 
        CreateMap<IProjectCreator, ProjectCreatorDto>(); 
        
        CreateMap<Project, ProjectDto>()
            .ConvertUsing((src, _, ctx) => new ProjectDto
        {
            Id = src.Id,
            Name = src.Name,
            Status = src.Status,
            Description = src.Description,
            Details = ctx.Mapper.Map<ProjectDetailsDto>(src.ProjectDetails),
            Creator = ctx.Mapper.Map<ProjectCreatorDto>(src.ProjectCreator),
          
        });
    }
}