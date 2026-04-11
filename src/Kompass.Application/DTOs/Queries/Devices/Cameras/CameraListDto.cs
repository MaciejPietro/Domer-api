using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;

namespace Kompass.Application.DTOs.Queries.Devices.Cameras;

public class CameraListDto
{
    public DeviceId Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Description { get; set; }
}