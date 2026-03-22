using FluentValidation;
using Kompass.Application.Common.Validation;
using Kompass.Domain.Interfaces.Folders;

namespace Kompass.Application.Commands.Folder.DeleteFolder;

public class DeleteFolderCommandValidator : AbstractValidator<DeleteFolderCommand>
{
    public DeleteFolderCommandValidator(IFolderRepository folderRepository)
    {
        RuleFor(x => x.Id).Cascade(CascadeMode.Stop).MustBeGuidObject().MustFolderExists(folderRepository);
    }
}