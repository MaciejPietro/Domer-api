using FluentValidation;
using JetBrains.Annotations;

namespace Kompass.Application.Commands.Device.Camera;

public class CreateCameraCommandValidator : AbstractValidator<CreateCameraCommand>
{
    public CreateCameraCommandValidator()
    {
        // NAME
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");
        
        // NAME
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));
        
        // VERTICAL ANGLE
        RuleFor(x => x.VerticalAngle)
            .InclusiveBetween(0, 360).WithMessage("VerticalAngle must be between 0 and 360 (inclusive).")
            .When(x => x.VerticalAngle is not null);
        
        // HORIZONTAL ANGLE
        RuleFor(x => x.HorizontalAngle)
            .InclusiveBetween(0, 360).WithMessage("HorizontalAngle must be between 0 and 360 (inclusive).")
            .When(x => x.HorizontalAngle is not null);
        
        // MAX DISTANCE
        RuleFor(x => x.MaxDistance)
            .InclusiveBetween(0, 1000).WithMessage("MaxDistance must be between 0 and 1000 (inclusive).")
            .When(x => x.HorizontalAngle is not null);
    }
}