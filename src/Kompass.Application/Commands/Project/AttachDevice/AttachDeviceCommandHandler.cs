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
            await _projectRepository.AttachDevice(request.ProjectId, request.DeviceId, cancellationToken);
        
            return Result<Unit>.Success(Unit.Value);
        } catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}