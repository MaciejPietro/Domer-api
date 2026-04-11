using FluentValidation;
using JetBrains.Annotations;
using Kompass.Domain.ValueObjects.Device;

namespace Kompass.Application.Commands.Device.Camera;

public class CreateCameraCommandValidator : AbstractValidator<CreateCameraCommand>
{
    public CreateCameraCommandValidator()
    {
        const int minAngle = CameraConfiguration.ValidationRules.MinAngle;
        const int maxAngle = CameraConfiguration.ValidationRules.MaxAngle;
        
        const int minDistance = CameraConfiguration.ValidationRules.MinDistance;
        const int maxDistance = CameraConfiguration.ValidationRules.MaxDistance;

        
        // NAME
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");
        
        // NAME
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));
        
        // VERTICAL ANGLE
        RuleFor(x => x.VerticalAngle)
            .InclusiveBetween(minAngle, maxAngle).WithMessage($"VerticalAngle must be between {minAngle} and {maxAngle} (inclusive).")
            .When(x => x.VerticalAngle is not null);
        
        // HORIZONTAL ANGLE
        RuleFor(x => x.HorizontalAngle)
            .InclusiveBetween(minAngle, maxAngle).WithMessage($"HorizontalAngle must be between {minAngle} and {maxAngle} (inclusive).")
            .When(x => x.HorizontalAngle is not null);
        
        // MAX DISTANCE
        RuleFor(x => x.MaxDistance)
            .InclusiveBetween(minDistance, maxDistance).WithMessage($"MaxDistance must be between {minDistance} and {maxDistance} (inclusive).")
            .When(x => x.HorizontalAngle is not null);
    }
}