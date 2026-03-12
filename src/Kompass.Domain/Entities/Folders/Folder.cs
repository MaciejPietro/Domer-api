using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces.Folders;
using MailKit;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Document = Kompass.Domain.Entities.Documents.Document;

namespace Kompass.Domain.Entities.Folders;

public class Folder: Entity<FolderId>, IFolder
{
    public override FolderId Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Self-referencing: null = root folder in project
    public FolderId? ParentFolderId { get; set; }
    public Folder? ParentFolder { get; set; }

    public ProjectId ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public ICollection<Folder> SubFolders { get; set; } = [];
    public ICollection<Document> Documents { get; set; } = [];
}