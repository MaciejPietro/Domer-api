using FluentValidation;
using Kompass.Application.Common.Validation;
using Kompass.Domain.Interfaces.Devices;

namespace Kompass.Application.Commands.Device.Camera.DeleteCamera;

public class DeleteCameraCommandValidator: AbstractValidator<DeleteCameraCommand>
{
    public DeleteCameraCommandValidator(IDeviceRepository deviceRepository)
    {
        RuleFor(x => x.Id).Cascade(CascadeMode.Stop).MustBeGuidObject();
    }
}