using Kompass.Domain.Common;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Entities.Folders;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Domain.Interfaces.Documents;

public interface IDocumentRepository
{
    Task<IDocument> AddAsync(Document document, CancellationToken cancellationToken);

    
    Task<bool> DeleteAsync( DocumentId documentId,  CancellationToken cancellationToken);
}