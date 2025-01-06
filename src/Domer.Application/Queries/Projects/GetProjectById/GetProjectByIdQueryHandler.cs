using AutoMapper;
using Domer.Application.Common.Responses;
using Domer.Application.DTOs.Queries;
using Domer.Domain.Entities.Projects;
using Domer.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Queries.Projects.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ResultResponse<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    
    public GetProjectByIdQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ResultResponse<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IProject project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
            
            ProjectDto? projectDto = _mapper.Map<ProjectDto>(project);
            return new ResultResponse<ProjectDto> { Result = projectDto };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

}