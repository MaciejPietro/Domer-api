using FluentValidation;
using Kompass.Application.Common.Validation;
using Kompass.Domain.Interfaces.Folders;
using Kompass.Domain.Interfaces.Projects;
using System.Threading.Tasks;

namespace Kompass.Application.Commands.Folder.CreateFolder;

public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
{
    public CreateFolderCommandValidator(
        IProjectRepository projectRepository,
        IFolderRepository folderRepository)
    {
        // NAME
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        
        // DUPLICATED NAME
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustFolderHaveNoDuplicatedNames(x => Task.FromResult(x.ParentFolderId),x => x.Name!, folderRepository)
            .When(x => !string.IsNullOrEmpty(x.ParentFolderId));

        // PROJECT ID
        RuleFor(x => x.ProjectId)
            .Cascade(CascadeMode.Stop)
            .MustBeGuidObject()
            .MustProjectExists(projectRepository);

        // PARENT FOLDER ID (if provided)
        RuleFor(x => x.ParentFolderId)
            .Cascade(CascadeMode.Stop)
            .MustBeGuidObject().When(x => !string.IsNullOrEmpty(x.ParentFolderId))
            .MustFolderExists(folderRepository).When(x => !string.IsNullOrEmpty(x.ParentFolderId));
        
        // PARENT FOLDER ID belongs to PROJECT ID
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustBelongsToProject(
                x => x.ParentFolderId,
                x => x.ProjectId,
                folderRepository)
            .When(x => !string.IsNullOrEmpty(x.ParentFolderId));

        
        // TODO Max Depth reached, validation
    }
}