using Kompass.Domain.Common;

namespace Kompass.Application.DTOs.Queries.Devices.Cameras;

public class CameraDto
{
    public CameraId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int VerticalAngle { get; set; }
    public int HorizontalAngle { get; set; }
    public int MaxDistance { get; set; }
}