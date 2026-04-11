using System;

namespace Kompass.Domain.ValueObjects.Device;

public class CameraConfiguration
{
    /// <summary>
    /// Single source of truth for camera validation rules.
    /// Used by both domain entity (Camera.cs) and application layer (validator).
    /// </summary>
    public static class ValidationRules
    {
        public const int MinAngle = 0;
        public const int MaxAngle = 360;
        public static void ValidateAngle(Angle angle)
        {
            if (angle.Value is < MinAngle or > MaxAngle)
                throw new ArgumentException($"Angle must be between {MinAngle} and {MaxAngle} degrees (inclusive).");
        }

        public const int MinDistance = 1;
        public const int MaxDistance = 1000;
        public static void ValidateDistance(Centimeter distance)
        {
            if (distance.Value is < MinDistance or > MaxDistance)
                throw new ArgumentException($"MaxDistance must be between {MinDistance} and {MaxDistance} meters (inclusive).", nameof(distance));
        }
    }

    public Angle VerticalAngle { get; }
    public Angle HorizontalAngle { get; }
    public Centimeter MaxDistance { get; }

    public CameraConfiguration(Angle? verticalAngle, Angle? horizontalAngle, Centimeter? maxDistance)
    {
        VerticalAngle = verticalAngle ?? new Angle(0);
        HorizontalAngle = horizontalAngle ?? new Angle(0);
        MaxDistance = maxDistance ?? new Centimeter(1);
        
        ValidationRules.ValidateAngle(VerticalAngle);
        ValidationRules.ValidateAngle(HorizontalAngle);
        ValidationRules.ValidateDistance(MaxDistance);
    }
}