using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.ValueObjects;

namespace Kompass.Domain.Interfaces.Devices.Camera;

public interface ICamera
{
    CameraId Id { get; }
    
    DeviceId DeviceId { get; }
    Device Device { get; }
    
    Angle HorizontalAngle { get; }
    Angle VerticalAngle { get; }
    Centimeter MaxDistance { get; }
}