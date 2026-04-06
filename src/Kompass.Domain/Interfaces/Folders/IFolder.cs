using Kompass.Domain.Common;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Entities.Projects;
using System;
using System.Collections.Generic;

namespace Kompass.Domain.Interfaces.Folders;

public interface IFolder
{
    FolderId Id { get; }
    string Name { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }

    // Self-referencing: null = root folder in project
    FolderId? ParentFolderId { get; set; }
    Folder? ParentFolder { get; set; }

    ProjectId ProjectId { get; set; }
    Project Project { get; set; }

    ICollection<Folder> SubFolders { get; set; }
    ICollection<Document> Documents { get; set; }
}