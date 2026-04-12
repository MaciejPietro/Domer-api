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
        
        var isSuccess = await _deviceRepository.DeleteAsync(deviceId, cancellationToken);

        return isSuccess ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Invalid();
    }
}