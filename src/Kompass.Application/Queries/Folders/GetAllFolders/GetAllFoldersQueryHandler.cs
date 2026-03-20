using Kompass.Application.DTOs.Queries.Folders;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Interfaces.Folders;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Queries.Folders.GetAllFolders;

public class GetAllFoldersQueryHandler : IRequestHandler<GetAllFoldersQuery, List<FolderListDto>>
{
    private readonly IFolderRepository _folderRepository;

    public GetAllFoldersQueryHandler(IFolderRepository folderRepository)
    {
        _folderRepository = folderRepository;
    }

    public async Task<List<FolderListDto>> Handle(GetAllFoldersQuery request, CancellationToken cancellationToken)
    {
        var (folders, _) = await _folderRepository.GetAllAsync(
            request.ProjectId!,
            cancellationToken);

        return folders.Select(MapToDto).ToList();
    }

    private static FolderListDto MapToDto(Folder folder)
    {
        return new FolderListDto
        {
            Id = folder.Id,
            Name = folder.Name,
            SubFolders = folder.SubFolders
                .OrderByDescending(f => f.CreatedAt)
                .Select(MapToDto)
                .ToList()
        };
    }
}
