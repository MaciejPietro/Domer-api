using Kompass.Domain.Common;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Interfaces.Folders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<Folder?> GetByIdAsync(FolderId folderId, CancellationToken cancellationToken)
    {
        return await dbContext.Folders
            .FirstOrDefaultAsync(f => f.Id == folderId, cancellationToken);
    }



    public async Task<Folder> GetAllAsync(FolderId folderId, CancellationToken cancellationToken)
    {
        var folder = await dbContext.Folders
            .FirstOrDefaultAsync(f => f.Id == folderId, cancellationToken)
            ?? throw new InvalidOperationException($"Folder with id {folderId} not found.");

        // Load all folders in the same project into the change tracker.
        // EF Core fix-up will automatically wire SubFolders navigation properties,
        // giving us the full recursive tree without manual recursion.
        await dbContext.Folders
            .Where(f => f.ProjectId == folder.ProjectId)
            .LoadAsync(cancellationToken);

        return folder;
    }

    public async Task<(List<Folder> Folders, int TotalCount)> GetAllAsync(
        ProjectId projectId,
        CancellationToken cancellationToken)
    {
        // Load all folders for the project into the change tracker.
        // EF Core fix-up wires SubFolders navigation properties automatically.
        await dbContext.Folders
            .Where(f => f.ProjectId == projectId)
            .LoadAsync(cancellationToken);

        // Return only root-level folders; children are nested via SubFolders.
        var rootFolders = dbContext.Folders.Local
            .Where(f => f.ProjectId == projectId && f.ParentFolderId == null)
            .OrderByDescending(f => f.CreatedAt)
            .ToList();

        return (rootFolders, rootFolders.Count);
    }

    public Task<bool> DeleteAsync(FolderId projectId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}