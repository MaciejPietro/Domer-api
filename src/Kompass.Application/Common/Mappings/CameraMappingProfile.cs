using AutoMapper;
using Kompass.Application.DTOs.Queries.Devices.Cameras;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Entities.Devices.Camera;

namespace Kompass.Application.Common.Mappings;

public class CameraMappingProfile : Profile
{
    public CameraMappingProfile()
    {
        CreateMap<(Device device, Camera camera), CameraDto>()
            .ConvertUsing(src => new CameraDto
            {
                Id = src.camera.Id,
                Name = src.device.Name,
                Description = src.device.Description,
                VerticalAngle = src.camera.VerticalAngle.Value,
                HorizontalAngle = src.camera.HorizontalAngle.Value,
                MaxDistance = src.camera.MaxDistance.Value
            });
    }
}
