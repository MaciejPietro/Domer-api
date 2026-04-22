using Ardalis.Result;
using AutoMapper;
using Kompass.Application.DTOs.Queries.Projects;
using Kompass.Domain.Common;
using Kompass.Domain.Interfaces.Projects;
using MediatR;
using System;
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
        if (!Guid.TryParse(request.Id, out var guid))
        {
            return Result.Invalid(new ValidationError("Id must be a valid GUID."));
        }

        var projectId = new ProjectId(guid);
        IProject? project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

        if (project == null)
        {
            return Result.NotFound();
        }

        ProjectDto? projectDto = _mapper.Map<ProjectDto>(project);
        return Result.Success(projectDto);
    }

}