using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Interfaces.Devices.Camera;
using Kompass.Domain.ValueObjects;
using System;

namespace Kompass.Domain.Entities.Devices.Camera;

public class Camera: Entity<CameraId>, ICamera
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
        if (horizontalAngle.Value is < 0 or > 360)
            throw new ArgumentException("HorizontalAngle must be greater than 0 and less than or equal to 360 degrees", nameof(horizontalAngle));

        if (verticalAngle.Value is < 0 or > 360)
            throw new ArgumentException("VerticalAngle must be greater than 0 and less than or equal to 360 degrees", nameof(verticalAngle));

        if (maxDistance.Value <= 0)
            throw new ArgumentException("MaxDistance must be greater than 0 meters", nameof(maxDistance));

        Camera camera = new() { DeviceId = deviceId, HorizontalAngle = horizontalAngle, VerticalAngle = verticalAngle, MaxDistance = maxDistance };

        return camera;
    }
}