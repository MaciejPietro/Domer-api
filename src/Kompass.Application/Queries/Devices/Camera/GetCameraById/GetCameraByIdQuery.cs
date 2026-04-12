using Ardalis.Result;
using Kompass.Application.DTOs.Queries.Devices.Cameras;
using MediatR;

namespace Kompass.Application.Queries.Devices.Camera.GetCameraById;

public record GetCameraByIdQuery(string? Id) : IRequest<Result<CameraDto>>;
