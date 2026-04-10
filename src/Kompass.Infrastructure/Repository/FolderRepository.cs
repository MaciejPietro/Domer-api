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

    public async Task<IFolder> UpdateAsync(IFolder folder, CancellationToken cancellationToken)
    {
        dbContext.Folders.Update((Folder)folder);
        await dbContext.SaveChangesAsync(cancellationToken);
        return folder;
    }

    public async Task<Folder?> GetByIdAsync(FolderId folderId, CancellationToken cancellationToken)
    {
        return await dbContext.Folders
            .FirstOrDefaultAsync(f => f.Id == folderId, cancellationToken);
    }

    public async Task<Folder?> GetByNameAsync(string folderName, FolderId? folderId, CancellationToken cancellationToken)
    {
        var query = dbContext.Folders.Where(f => f.Name == folderName);

        if (folderId is not null)
        {
            query = query.Where(f => f.ParentFolderId == folderId);
        }

        var folder = await query.FirstOrDefaultAsync(cancellationToken);

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

    // TODO think about better solution since it is ai generated and I think it coulde be better
    public async Task<bool> DeleteAsync(FolderId folderId, CancellationToken cancellationToken)
    {
        var folder = await dbContext.Folders
            .FindAsync([folderId], cancellationToken);

        if (folder == null)
        {
            return false;
        }

        // Load entire subtree so EF can remove children before parents
        var allDescendants = new List<Folder>();
        await LoadDescendantsAsync(folderId, allDescendants, cancellationToken);

        // Remove deepest children first, then the folder itself
        allDescendants.Reverse();
        dbContext.Folders.RemoveRange(allDescendants);
        dbContext.Folders.Remove(folder);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task LoadDescendantsAsync(FolderId parentId, List<Folder> result, CancellationToken cancellationToken)
    {
        var children = await dbContext.Folders
            .Where(f => f.ParentFolderId == parentId)
            .ToListAsync(cancellationToken);

        foreach (var child in children)
        {
            await LoadDescendantsAsync(child.Id, result, cancellationToken);
            result.Add(child);
        }
    }
}