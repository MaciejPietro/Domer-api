using Ardalis.Result;
using Kompass.Application.Services.Devices;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.ValueObjects;
using Kompass.Domain.ValueObjects.Device;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Device.Camera.CreateCamera;

public class CreateCameraCommandHandler : IRequestHandler<CreateCameraCommand, Result<Unit>>
{
    private readonly IDeviceService _deviceService;
    
    public CreateCameraCommandHandler(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    
    public async Task<Result<Unit>> Handle(CreateCameraCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var angle = request.VerticalAngle is null ? null : new Angle(request.VerticalAngle.Value);
            var horizontalAngle = request.HorizontalAngle is null ? null : new Angle(request.HorizontalAngle.Value);
            var maxDistance = request.MaxDistance is null ? null : new Centimeter(request.MaxDistance.Value);
            
            var config = new CameraConfiguration(angle, horizontalAngle, maxDistance);
            
            await _deviceService.CreateDeviceAsync(DeviceType.Camera, request.Name, request.Description, config, cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}
