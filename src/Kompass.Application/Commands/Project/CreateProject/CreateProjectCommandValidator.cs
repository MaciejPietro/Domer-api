using FluentValidation;
using Kompass.Domain.ValueObjects.Common;
using Kompass.Domain.ValueObjects.Project;

namespace Kompass.Application.Commands.Project.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        // NAME
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(ProjectConfiguration.ValidationRules.NameMaxLength).WithMessage("Name must not exceed 100 characters.");
        
        // NAME
        RuleFor(x => x.Description)
            .MaximumLength(ProjectConfiguration.ValidationRules.DescriptionMaxLength)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must not exceed 100 characters.");
        
        // URLS
        When(x => x.Urls != null, () =>
        {
            RuleForEach(x => x.Urls)
                .ChildRules(url =>
                {
                    url.RuleFor(x => x.Url).NotEmpty().MaximumLength(UrlConfiguration.ValidationRules.UrlMaxLength);
                    url.RuleFor(x => x.Name).NotEmpty().MaximumLength(UrlConfiguration.ValidationRules.NameMaxLength);
                });
        });
        
        // IMAGES
        // RuleForEach(x => x.Images)
        //     .Must(file => file.Length <= 2 * 1024 * 1024) // 2MB limit
        //     .WithMessage("Image size must be less than 2MB")
        //     .Must(file => new[] { ".jpg", ".jpeg", ".png" }
        //         .Contains(Path.GetExtension(file.FileName).ToLower()))
        //     .WithMessage("Only .jpg and .png files are allowed");
        // RuleFor(x => x.Images)
        //     .Must(x => x == null || x.Count <= 10)
        //     .WithMessage("Maximum 10 images allowed");

    }
}