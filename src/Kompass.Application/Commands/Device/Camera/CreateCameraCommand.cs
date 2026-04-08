namespace Kompass.Application.Commands.Device.Camera;

public class CreateCameraCommand
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public float? HorizontalAngle { get; init; }
    public float? VerticalAngle { get; init; }
    public float? MaxDistance { get; init; }
}