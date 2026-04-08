using System;

namespace Kompass.Domain.ValueObjects;

public record Angle
{
    public int Value { get; init; }

    public Angle(int value)
    {
        if (value is < 0 or > 360)
            throw new ArgumentException("Angle must be between 0 and 360 degrees");

        Value = value;
    }
}