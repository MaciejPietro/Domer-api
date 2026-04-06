using Kompass.Domain.Common;
using Kompass.Domain.Common.Entities;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Interfaces.Documents;
using System;

namespace Kompass.Domain.Entities.Documents;

public class Document: Entity<DocumentId>, IDocument
{
    public override DocumentId Id { get; protected set; } = Guid.CreateVersion7(); 
    public string Title { get; set; } = string.Empty;       // e.g. "fines.md"
    public string Content { get; set; } = string.Empty;      // markdown body
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public FolderId FolderId { get; set; }
    public Folder Folder { get; set; } = null!;
}