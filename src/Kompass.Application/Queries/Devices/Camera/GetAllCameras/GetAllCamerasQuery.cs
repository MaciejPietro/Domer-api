using Kompass.Application.DTOs.Queries.Devices.Cameras;
using Kompass.Domain.Common;
using MediatR;
using System.Collections.Generic;

namespace Kompass.Application.Queries.Devices.Camera.GetAllCameras;

public class GetAllCamerasQuery : IRequest<List<CameraListDto>>
{
    public CameraId CameraId { get; init; }    
}