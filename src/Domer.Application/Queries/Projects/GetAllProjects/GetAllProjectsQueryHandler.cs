using Domer.Application.DTOs.Queries;
using Domer.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Queries.Projects.GetAllProjects;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectListDto>>
{
    private readonly IProjectRepository _projectRepository;
    
    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<List<ProjectListDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var results = await _projectRepository.GetAllAsync(1, cancellationToken);

            return results.Select(project => new ProjectListDto
            {
                Id = project.Id,
                Name = project.Name,
                UsableArea = project.UsableArea,
                BuildingArea = project.BuildingArea,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            }).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
       
    }


}