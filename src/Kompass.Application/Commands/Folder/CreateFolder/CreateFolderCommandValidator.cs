using FluentValidation;
using Kompass.Application.Common.Validation;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Folder.CreateFolder;

public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IFolderRepository _folderRepository;

    public CreateFolderCommandValidator(
        IProjectRepository projectRepository,
        IFolderRepository folderRepository)
    {
        _projectRepository = projectRepository;
        _folderRepository = folderRepository;
        
        // NAME
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");


        // PROJECT ID
        RuleFor(x => x.ProjectId)
            .Cascade(CascadeMode.Stop)
            .MustBeGuidObject()
        .MustAsync(async (projectId, cancellation) =>
            {
                Guid.TryParse(projectId.ToString(), out Guid guid);
            Domain.Entities.Projects.Project? project = await _projectRepository.GetByIdAsync(guid, cancellation);
            return project != null;
        })
        .WithMessage("Project does not exist.");


        // PARENT FOLDER ID (if provided)
        // RuleFor(x => x.ParentFolderId)
        //     .MustBeGuid()
        //     .When(x => !string.IsNullOrEmpty(x.ParentFolderId))
        //     .MustAsync(async (parentFolderId, cancellation) =>
        //         await ParentFolderExists(parentFolderId, cancellation))
        //     .When(x => !string.IsNullOrEmpty(x.ParentFolderId))
        //     .WithMessage("Parent folder does not exist.");
        //
        // // PARENT FOLDER MUST BELONG TO SAME PROJECT
        // RuleFor(x => x)
        //     .MustAsync(async (command, cancellation) =>
        //         await ParentFolderBelongsToProject(command.ParentFolderId, command.ProjectId, cancellation))
        //     .When(x => !string.IsNullOrEmpty(x.ParentFolderId))
        //     .WithMessage("Parent folder must belong to the same project.");
    }

    // private async Task<bool> ParentFolderExists(string? parentFolderId, CancellationToken cancellationToken)
    // {
    //     if (string.IsNullOrEmpty(parentFolderId) || !Guid.TryParse(parentFolderId, out var guid))
    //         return false;
    //
    //     var folder = await _folderRepository.GetByIdAsync(new Domain.Common.FolderId(guid), cancellationToken);
    //     return folder != null;
    // }
    //
    // private async Task<bool> ParentFolderBelongsToProject(
    //     string? parentFolderId,
    //     string? projectId,
    //     CancellationToken cancellationToken)
    // {
    //     if (string.IsNullOrEmpty(parentFolderId) || !Guid.TryParse(parentFolderId, out var parentGuid))
    //         return true;
    //
    //     if (string.IsNullOrEmpty(projectId) || !Guid.TryParse(projectId, out var projectGuid))
    //         return true;
    //
    //     var parentFolder = await _folderRepository.GetByIdAsync(new Domain.Common.FolderId(parentGuid), cancellationToken);
    //     return parentFolder?.ProjectId == new Domain.Common.ProjectId(projectGuid);
    // }
}