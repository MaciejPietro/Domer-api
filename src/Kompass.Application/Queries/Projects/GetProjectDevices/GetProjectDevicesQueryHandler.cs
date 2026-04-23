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
            
            var devices =  await _projectRepository.GetDevicesAsync(projectId, cancellationToken);

            if (devices.Count is not 0)
            {
                return Result.Success(devices);
            }

            var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

            return project is null ? Result.NotFound($"Project with ID '{projectId}' was not found.") : Result.Success(devices);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

}