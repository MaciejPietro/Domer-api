using Kompass.Domain.Common;
using System.Collections.Generic;

namespace Kompass.Application.DTOs.Queries.Folders;

public class FolderListDto
{
    public FolderId Id { get; set; }
    public string Name { get; set; }
    public List<FolderListDto> SubFolders { get; set; } = new();
}