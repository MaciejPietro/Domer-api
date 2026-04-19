using Ardalis.Result;
using AutoMapper;
using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries;
using Kompass.Application.DTOs.Queries.Projects;
using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Queries.Projects.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    
    public GetProjectByIdQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var projectId = new ProjectId(Guid.Parse(request.Id!));
        
            IProject? project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

            if (project == null)
            {
                return Result.NotFound();
            }
            
            
            ProjectDto? projectDto = _mapper.Map<ProjectDto>(project);
            return Result.Success(projectDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

}