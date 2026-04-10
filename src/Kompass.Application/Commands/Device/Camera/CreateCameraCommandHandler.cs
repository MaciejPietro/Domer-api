using Ardalis.Result;
using Kompass.Application.Services.Devices;
using Kompass.Domain.Enums.Devices;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Device.Camera;

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
            await _deviceService.CreateDeviceAsync(DeviceType.Camera, request.Name, request.Description, cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Error(e.Message);
        }
    }
}
