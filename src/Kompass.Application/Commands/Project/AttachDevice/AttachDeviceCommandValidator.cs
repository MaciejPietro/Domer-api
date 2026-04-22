using FluentValidation;
using Kompass.Application.Common.Validation;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.Interfaces.Projects;

namespace Kompass.Application.Commands.Project.AttachDevice;

public class AttachDeviceCommandValidator : AbstractValidator<AttachDeviceCommand>
{
    public AttachDeviceCommandValidator(IProjectRepository projectRepository, IDeviceRepository deviceRepository)
    {
            RuleLevelCascadeMode = ClassLevelCascadeMode;
            
            // PROJECT ID
            RuleFor(x => x.ProjectId)
                .Cascade(CascadeMode.Stop)
                .MustBeGuidObject()
                .MustProjectExists(projectRepository);
            
            // DEVICE ID
            RuleFor(x => x.DeviceId).Cascade(CascadeMode.Stop).MustBeGuidObject().MustDeviceExists(deviceRepository);
    }
}