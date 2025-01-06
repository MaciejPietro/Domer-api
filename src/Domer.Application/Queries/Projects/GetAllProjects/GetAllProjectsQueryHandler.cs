using Domer.Application.Common.Responses;
using Domer.Application.DTOs.Queries;
using Domer.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Queries.Projects.GetAllProjects;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, PaginatedResponse<ProjectListDto>>
{
    private readonly IProjectRepository _projectRepository;
    
    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<PaginatedResponse<ProjectListDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var (projects, totalCount) = await _projectRepository.GetAllAsync(
                request.Page,
                request.PerPage,
                cancellationToken);

            var items = projects.Select(project => new ProjectListDto
            {
                Id = project.Id,
                Name = project.Name,
                UsableArea = project.UsableArea,
                BuildingArea = project.BuildingArea,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            }).ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PerPage);

            return new PaginatedResponse<ProjectListDto>
            {
                Results = items,
                CurrentPage = request.Page,
                TotalPages = totalPages,
                TotalItems = totalCount,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
}