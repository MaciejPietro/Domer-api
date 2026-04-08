using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Interfaces.Devices.Camera;
using System;

namespace Kompass.Domain.Entities.Devices.Camera;

public class Camera: Entity<CameraId>, ICamera
{
    private Camera() {}

    public override CameraId Id { get; protected set; } = Guid.CreateVersion7();
    public DeviceId DeviceId { get; init; }
    public Device Device { get; init; }
    public float HorizontalAngle { get; init; }
    public float VerticalAngle { get; init; }
    public float MaxDistance { get; init;}

    public static Camera Create(DeviceId deviceId, float horizontalAngle, float verticalAngle, float maxDistance)
    {
        if (horizontalAngle <= 0 || horizontalAngle > 360)
            throw new ArgumentException("HorizontalAngle must be greater than 0 and less than or equal to 360 degrees", nameof(horizontalAngle));

        if (verticalAngle <= 0 || verticalAngle > 360)
            throw new ArgumentException("VerticalAngle must be greater than 0 and less than or equal to 360 degrees", nameof(verticalAngle));

        if (maxDistance <= 0)
            throw new ArgumentException("MaxDistance must be greater than 0 meters", nameof(maxDistance));

        Camera camera = new() { DeviceId = deviceId, HorizontalAngle = horizontalAngle, VerticalAngle = verticalAngle, MaxDistance = maxDistance };

        return camera;
    }
}