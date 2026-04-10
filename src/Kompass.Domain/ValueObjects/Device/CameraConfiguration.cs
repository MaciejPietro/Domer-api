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
        public const int MinDistance = 1;
        public const int MaxDistance = 1000;

        public static void ValidateAngle(Angle angle, string paramName)
        {
            if (angle.Value < MinAngle || angle.Value > MaxAngle)
                throw new ArgumentException($"{paramName} must be between {MinAngle} and {MaxAngle} degrees (inclusive).", paramName);
        }

        public static void ValidateDistance(Centimeter distance)
        {
            if (distance.Value <= MinDistance || distance.Value > MaxDistance)
                throw new ArgumentException($"MaxDistance must be greater than {MinDistance} meters and less than or equal to {MaxDistance} meters.", nameof(distance));
        }
    }

    public Angle VerticalAngle { get; }
    public Angle HorizontalAngle { get; }
    public Centimeter MaxDistance { get; }

    private CameraConfiguration() { }

    public CameraConfiguration(Angle verticalAngle, Angle? horizontalAngle, Centimeter? maxDistance)
    {
        VerticalAngle = verticalAngle ?? new Angle(0);
        HorizontalAngle = horizontalAngle ?? new Angle(0);
        MaxDistance = maxDistance ?? new Centimeter(0);

        // Validate all values when constructing - fail fast principle
        ValidationRules.ValidateAngle(VerticalAngle, nameof(VerticalAngle));
        ValidationRules.ValidateAngle(HorizontalAngle, nameof(HorizontalAngle));
        ValidationRules.ValidateDistance(MaxDistance);
    }
}