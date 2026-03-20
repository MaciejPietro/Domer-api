using Kompass.Application.DTOs.Queries.Folders;
using Kompass.Domain.Common;
using MediatR;
using System.Collections.Generic;

namespace Kompass.Application.Queries.Folders.GetAllFolders;

public class GetAllFoldersQuery : IRequest<List<FolderListDto>>
{
    public ProjectId ProjectId { get; set; }
}