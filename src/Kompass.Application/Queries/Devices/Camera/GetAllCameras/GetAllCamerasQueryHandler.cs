using Kompass.Application.DTOs.Queries.Devices.Cameras;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Enums.Devices;
using Kompass.Domain.Interfaces.Devices;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Queries.Devices.Camera.GetAllCameras;

public class GetAllCamerasQueryHandler : IRequestHandler<GetAllCamerasQuery, List<CameraListDto>>
{
    private readonly IDeviceRepository _deviceRepository;

    public GetAllCamerasQueryHandler(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }
    
    public async Task<List<CameraListDto>> Handle(GetAllCamerasQuery request, CancellationToken cancellationToken)
    {
        (IEnumerable<IDevice> cameras, _) = await _deviceRepository.GetAllAsync(DeviceType.Camera, cancellationToken);

        return new List<CameraListDto>(cameras.Select(d => new CameraListDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description
        }));
    }
}