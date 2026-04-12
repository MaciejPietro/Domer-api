using FluentValidation;
using Kompass.Application.Common.Validation;
using Kompass.Domain.Interfaces.Devices;

namespace Kompass.Application.Queries.Devices.Camera.GetCameraById;

public class GetCameraByIdQueryValidator: AbstractValidator<GetCameraByIdQuery>
{
    public GetCameraByIdQueryValidator(IDeviceRepository deviceRepository)
    {
        RuleFor(x => x.Id).Cascade(CascadeMode.Stop).MustBeGuidObject().MustDeviceExists(deviceRepository);
    }
}