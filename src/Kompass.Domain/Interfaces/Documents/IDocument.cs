using Kompass.Domain.Common;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Entities.Projects;
using System;
using System.Collections.Generic;

namespace Kompass.Domain.Interfaces.Documents;

public interface IDocument
{
    DocumentId Id { get;  }
    string Title { get; set; }
    string Content { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }

    FolderId FolderId { get; set; }
    Folder Folder { get; set; }

    
}