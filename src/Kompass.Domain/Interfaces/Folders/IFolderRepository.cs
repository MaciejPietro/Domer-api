using Kompass.Domain.Common;
using Kompass.Domain.Entities.Folders;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Domain.Interfaces.Folders;

public interface IFolderRepository
{
    Task<IFolder> AddAsync(Folder project, CancellationToken cancellationToken);

    Task<Folder> GetByIdAsync(FolderId folderId, CancellationToken cancellationToken);

    Task<Folder> GetAllAsync(FolderId folderId, CancellationToken cancellationToken);

    Task<(List<Folder> Folders, int TotalCount)> GetAllAsync(ProjectId projectId, CancellationToken cancellationToken);


    Task<bool> DeleteAsync( FolderId projectId,  CancellationToken cancellationToken);
}