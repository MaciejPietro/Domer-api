using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Device.Camera.CreateCamera;

public class CreateCameraCommand : IRequest<Result<Unit>>
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public int? HorizontalAngle { get; init; }
    public int? VerticalAngle { get; init; }
    public int? MaxDistance { get; init; }
}