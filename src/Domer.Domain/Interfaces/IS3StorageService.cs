using Domer.Domain.Models.S3Storage;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Domer.Domain.Interfaces;

public interface IS3StorageService
{
    Task<GetObjectModel> GetObjectAsync(string name);
    Task<UploadObjectModel> UploadObjectAsync(IFormFile file);
    Task<UploadObjectModel> RemoveObjectAsync(string fileName);
}