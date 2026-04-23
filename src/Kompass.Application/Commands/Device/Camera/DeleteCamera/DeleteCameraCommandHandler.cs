using Ardalis.Result;
using Kompass.Domain.Interfaces.Devices;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Device.Camera.DeleteCamera;

public class DeleteCameraCommandHandler :  IRequestHandler<DeleteCameraCommand, Result<Unit>>
{
    private readonly IDeviceRepository _deviceRepository;
    
    public DeleteCameraCommandHandler(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }
    
    public async Task<Result<Unit>> Handle(DeleteCameraCommand request, CancellationToken cancellationToken)
    {
        Guid.TryParse(request.Id, out Guid deviceId);
        
        var device = await _deviceRepository.DeleteAsync(deviceId, cancellationToken);

        if (device is null)
        {
            return Result.NotFound($"Device with ID {request.Id} not found");
        }

        return Result<Unit>.Success(Unit.Value);
    }
}