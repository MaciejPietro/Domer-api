using Ardalis.Result;
using AutoMapper;
using Kompass.Application.DTOs.Queries.Projects;
using Kompass.Application.Queries.Projects.GetProjectById;
using Kompass.Domain.Common;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Queries.Projects.GetProjectDevices;

public class GetProjectDevicesQueryHandler : IRequestHandler<GetProjectDevicesQuery, Result<List<ProjectDevice>>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    
    public GetProjectDevicesQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ProjectDevice>>> Handle(GetProjectDevicesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Guid.TryParse(request.Id, out Guid projectId);
            
            var device =  await _projectRepository.GetDevicesAsync(projectId, cancellationToken);
        
            // IProject? project = await _projectRepository.GetProjectDevicesAsync(projectId, cancellationToken);
            //
            // if (project == null)
            // {
            //     return Result.NotFound();
            // }
            //
            //
            // ProjectDto? projectDto = _mapper.Map<ProjectDto>(project);
            return Result.Success(device);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

}