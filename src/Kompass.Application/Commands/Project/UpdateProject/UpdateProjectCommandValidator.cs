using FluentValidation;
using Kompass.Application.Commands.Project.CreateProject;
using Kompass.Domain.ValueObjects.Common;
using Kompass.Domain.ValueObjects.Project;

namespace Kompass.Application.Commands.Project.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        // NAME
        RuleFor(x => x.Name)
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
    }
}