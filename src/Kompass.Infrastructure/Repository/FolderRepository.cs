using Kompass.Domain.Common;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Interfaces.Folders;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Infrastructure.Repository;

public class FolderRepository(ApplicationDbContext dbContext) : IFolderRepository
{
    public Task<IFolder> AddAsync(Folder project, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteAsync(FolderId projectId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}