using Ardalis.Result;
using AutoMapper;
using Kompass.Application.DTOs.Queries.Devices.Cameras;
using Kompass.Application.Services.Devices;
using Kompass.Domain.Common;
using Kompass.Domain.Interfaces.Devices;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Queries.Devices.Camera.GetCameraById;

public class GetCameraByIdQueryHandler : IRequestHandler<GetCameraByIdQuery, Result<CameraDto>>
{

    private readonly IDeviceService _deviceService;
    private readonly IMapper _mapper;

    public GetCameraByIdQueryHandler(IDeviceService deviceService, IMapper mapper)
    {
        _deviceService= deviceService;
        _mapper = mapper;
    }

    public async Task<Result<CameraDto>> Handle(GetCameraByIdQuery request, CancellationToken cancellationToken)
    {
        var deviceId = new DeviceId(Guid.Parse(request.Id));
        var (device, relatedEntity) = await _deviceService.GetDeviceWithRelatedEntityByIdAsync(deviceId, cancellationToken);

        if (device == null || relatedEntity == null)
        {
            return Result.NotFound();
        }
        CameraDto? cameraDto = _mapper.Map<CameraDto>((device, (Domain.Entities.Devices.Camera.Camera) relatedEntity));

        return Result.Success(cameraDto);
    }
}