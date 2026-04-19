using Kompass.Application.Services.Devices.Factories;
using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Services.Projects;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }



    // public async Task<(Device? device, IDeviceRelatedEntity? relatedEntity)> GetDeviceWithRelatedEntityByIdAsync(
    //     DeviceId deviceId,
    //     CancellationToken cancellationToken)
    // {
    //     var device = await _deviceRepository.GetByIdAsync(deviceId, cancellationToken);
    //
    //     if (device is null)
    //     {
    //         return (null, null);
    //     }
    //
    //     var relatedEntity = await _deviceRepository.GetRelatedEntityById(device.Type, deviceId, cancellationToken);
    //
    //     return (device, relatedEntity);
    //
    // }


    public async Task<(IProject? project, IProjectDetails? projectDetails, IProjectCreator? projectCreator)> GetProjectWithDetailsAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

        Console.WriteLine(project);
        
        
        throw new System.NotImplementedException();
    }
}