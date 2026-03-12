using Kompass.Domain.Common;
using Kompass.Domain.Entities.Documents;
using Kompass.Domain.Entities.Folders;
using Kompass.Domain.Interfaces.Documents;
using Kompass.Domain.Interfaces.Folders;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Infrastructure.Repository;

public class DocumentRepository(ApplicationDbContext dbContext) : IDocumentRepository
{
    public Task<IDocument> AddAsync(Document document, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteAsync(DocumentId documentId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}