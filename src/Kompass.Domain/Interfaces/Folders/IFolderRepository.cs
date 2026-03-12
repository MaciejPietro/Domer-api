using Kompass.Domain.Common;
using Kompass.Domain.Entities.Folders;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Domain.Interfaces.Folders;

public interface IFolderRepository
{
    Task<IFolder> AddAsync(Folder project, CancellationToken cancellationToken);

    
    Task<bool> DeleteAsync( FolderId projectId,  CancellationToken cancellationToken);
}