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
        
        // USABLE AREA
        When(x => x.UsableArea.HasValue, () =>
        {
            RuleFor(x => x.UsableArea)
                .GreaterThan(0);
        });

        // BUILDIN AREA
        When(x => x.BuildingArea.HasValue, () =>
        {
            RuleFor(x => x.BuildingArea)
                .GreaterThan(0);
        });

        // URLS
        When(x => x.Urls != null, () =>
        {
            RuleForEach(x => x.Urls)
                .ChildRules(url =>
                {
                    url.RuleFor(x => x.Url).NotEmpty().MaximumLength(2000);
                    url.RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
                });
        });
        
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