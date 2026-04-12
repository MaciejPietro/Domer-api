using FluentValidation;
using Kompass.Domain.Common;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Threading.Tasks;

namespace Kompass.Application.Common.Validation;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> MustBeValidEmail<T>(
        this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").WithMessage(value =>
            $"Email must be a valid email address.");
          
    }
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
    
    public static IRuleBuilderOptions<T, object?> MustDeviceExists<T>(
        this IRuleBuilder<T, object?> ruleBuilder, IDeviceRepository deviceRepository)
    {
        return ruleBuilder
            .MustAsync(async (deviceId, cancellation) =>
            {
                if (Guid.TryParse(deviceId?.ToString(), out Guid guid))
                {
                    
                    IDevice? folder = await deviceRepository.GetByIdAsync(guid, cancellation);
                    
                    return folder != null;
                }
                
                return false;
            })
            .WithMessage("Device does not exist.");
    }
    
    public static IRuleBuilderOptions<T, object?> MustFolderExists<T>(
        this IRuleBuilder<T, object?> ruleBuilder, IFolderRepository folderRepository)
    {
        return ruleBuilder
            .MustAsync(async (folderId, cancellation) =>
            {
                if (Guid.TryParse(folderId?.ToString(), out Guid guid))
                {
                    
                    Folder? folder = await folderRepository.GetByIdAsync(guid, cancellation);
                    
                    return folder != null;
                }
                
                return false;
            })
            .WithMessage("Folder does not exist.");
    }
    
    public static IRuleBuilderOptions<T, T> MustFolderHaveNoDuplicatedNames<T>(
        this IRuleBuilder<T, T> ruleBuilder,
        Func<T, Task<string?>> parentFolderIdSelector,
        Func<T, string> nameSelector,
        IFolderRepository folderRepository)
    {
        return ruleBuilder
            .MustAsync(async (command, cancellation) =>
            {
                string? parentFolderIdStr = await parentFolderIdSelector(command);
                Guid.TryParse(parentFolderIdStr, out Guid parentFolderId);
                string name = nameSelector(command);
               
                
                Folder? folder = await folderRepository.GetByNameAsync(name, parentFolderId, cancellation);

                return folder is null;
            })
            .WithMessage(x =>
                $"'{nameSelector(x)}' already exists in this folder.");
    }
    
    public static IRuleBuilderOptions<T, T> MustBelongsToProject<T>(
        this IRuleBuilder<T, T> ruleBuilder,
        Func<T, string?> parentFolderIdSelector,
        Func<T, object?> projectIdSelector,
        IFolderRepository folderRepository)
    {
        return ruleBuilder
            .MustAsync(async (command, cancellationToken) =>
            {
                if (!Guid.TryParse(parentFolderIdSelector(command), out Guid parentFolderId))
                {
                    return false;
                }

                if (!Guid.TryParse(projectIdSelector(command)?.ToString(), out Guid projectId))
                {
                    return false;
                }

                var parentFolder = await folderRepository.GetByIdAsync(new FolderId(parentFolderId), cancellationToken);

                return parentFolder.ProjectId == projectId;
            })
            .WithMessage("Folder and parent folder must belong to the same project.");
    }
}