using Kompass.Domain.Common;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Interfaces.Folders;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Infrastructure.Repository;

public class FolderRepository(ApplicationDbContext dbContext) : IFolderRepository
{
    public async Task<IFolder> AddAsync(Folder folder, CancellationToken cancellationToken)
    {
        await dbContext.Folders.AddAsync(folder, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return folder;
    }

    public async Task<Folder> GetByIdAsync(FolderId folderId, CancellationToken cancellationToken)
    {
        return await dbContext.Folders
            .FirstOrDefaultAsync(f => f.Id == folderId, cancellationToken);
    }

    public Task<bool> DeleteAsync(FolderId projectId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}