using FluentValidation;

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
    }
}