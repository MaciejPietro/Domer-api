using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.Interfaces.Devices.Camera;
using Kompass.Domain.ValueObjects;
using Kompass.Domain.ValueObjects.Device;
using System;

namespace Kompass.Domain.Entities.Devices.Camera;

public class Camera: Entity<CameraId>, ICamera, IDeviceRelatedEntity
{
    private Camera() {}
    public override CameraId Id { get; protected set; } = Guid.CreateVersion7();
    public DeviceId DeviceId { get; init; }
    public Device Device { get; init; }
    public Angle HorizontalAngle { get; init; }
    public Angle VerticalAngle { get; init; }
    public Centimeter MaxDistance { get; init;}

    public static Camera Create(DeviceId deviceId, Angle horizontalAngle, Angle verticalAngle, Centimeter maxDistance)
    {
        CameraConfiguration.ValidationRules.ValidateAngle(horizontalAngle);
        CameraConfiguration.ValidationRules.ValidateAngle(verticalAngle);
        CameraConfiguration.ValidationRules.ValidateDistance(maxDistance);

        Camera camera = new() { DeviceId = deviceId, HorizontalAngle = horizontalAngle, VerticalAngle = verticalAngle, MaxDistance = maxDistance };

        return camera;
    }

    
}