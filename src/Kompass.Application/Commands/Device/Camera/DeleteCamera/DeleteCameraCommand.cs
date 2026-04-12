using Ardalis.Result;
using MediatR;

namespace Kompass.Application.Commands.Device.Camera.DeleteCamera;

public class DeleteCameraCommand: IRequest<Result<Unit>>
{
    public string? Id { get; set; }
}