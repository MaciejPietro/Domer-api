using Ardalis.Result;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Project.AttachDevice;

public class AttachDeviceCommandHandler: IRequestHandler<AttachDeviceCommand, Result<Unit>>
{
    
    private readonly IProjectRepository _projectRepository;
    
    public AttachDeviceCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<Result<Unit>> Handle(AttachDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guid.TryParse(request.DeviceId, out Guid deviceId);
            Guid.TryParse(request.ProjectId, out Guid projectId);
            
            await _projectRepository.AttachDevice(projectId, deviceId, cancellationToken);
        
            return Result<Unit>.Success(Unit.Value);
        } catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}