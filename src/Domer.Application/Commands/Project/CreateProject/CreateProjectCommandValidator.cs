using FluentValidation;
using System.IO;
using System.Linq;

namespace Domer.Application.Commands.Project.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        // NAME
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        
        // IMAGES
        RuleForEach(x => x.Images)
            .Must(file => file.Length <= 2 * 1024 * 1024) // 2MB limit
            .WithMessage("Image size must be less than 2MB")
            .Must(file => new[] { ".jpg", ".jpeg", ".png" }
                .Contains(Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("Only .jpg and .png files are allowed");
        RuleFor(x => x.Images)
            .Must(x => x == null || x.Count <= 10)
            .WithMessage("Maximum 10 images allowed");

    }
}