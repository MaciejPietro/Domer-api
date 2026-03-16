using FluentValidation;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using System;

namespace Kompass.Application.Common.Validation;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, object?> MustBeGuidObject<T>(
        this IRuleBuilder<T, object?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"'{{PropertyName}}' is required")
            .Must(projectId => Guid.TryParse(projectId?.ToString(), out _))
            .WithMessage(value =>
                $"'{{PropertyName}}' must be a valid Guid");
    }
    
    public static IRuleBuilderOptions<T, object?> MustProjectExists<T>(
        this IRuleBuilder<T, object?> ruleBuilder, IProjectRepository projectRepository)
    {
        return ruleBuilder
            .MustAsync(async (projectId, cancellation) =>
            {
                if (Guid.TryParse(projectId?.ToString(), out Guid guid))
                {
                    
                    Domain.Entities.Projects.Project? project = await projectRepository.GetByIdAsync(guid, cancellation);
                    return project != null;
                }
                
                return false;
            })
            .WithMessage("Project does not exist.");
    }
    
    public static IRuleBuilderOptions<T, object?> MustFolderExists<T>(
        this IRuleBuilder<T, object?> ruleBuilder, IFolderRepository folderRepository)
    {
        return ruleBuilder
            .MustAsync(async (folderId, cancellation) =>
            {
                if (Guid.TryParse(folderId?.ToString(), out Guid guid))
                {
                    
                    var folder = await folderRepository.GetByIdAsync(guid, cancellation);
                    
                    return folder != null;
                }
                
                return false;
            })
            .WithMessage("Folder does not exist.");
    }
}