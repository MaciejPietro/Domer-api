using System.IO;

namespace Kompass.Domain.Models.S3Storage;

public class GetObjectModel
{
    public string ContentType { get; set; }
    public Stream Content { get; set; }
}