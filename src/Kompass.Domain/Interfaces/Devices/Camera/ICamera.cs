using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;

namespace Kompass.Domain.Interfaces.Devices.Camera;

public interface ICamera
{
    CameraId Id { get; }
    
    DeviceId DeviceId { get; }
    Device Device { get; }
    
    float HorizontalAngle { get; }
    float VerticalAngle { get; }
    float MaxDistance { get; }
}