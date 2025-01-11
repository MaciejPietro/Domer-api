using Amazon;
using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Domer.Domain.Interfaces;
using Domer.Domain.Models.S3Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Domer.Infrastructure.Services;


public class AmazonS3StorageService : IS3StorageService
{
    private readonly string _bucketName;
    private readonly IAmazonS3 _s3Client;

    public AmazonS3StorageService(IConfiguration configuration)
    {
        EnvService.Load();
        
        string accessKey = Environment.GetEnvironmentVariable("AWS_S3_KEY") ?? string.Empty;
        string accessSecret = Environment.GetEnvironmentVariable("AWS_S3_SECRET") ?? string.Empty;
        _bucketName = Environment.GetEnvironmentVariable("AWS_S3_NAME") ?? string.Empty;
        RegionEndpoint region = RegionEndpoint.EUCentral1;
        
        _s3Client = new AmazonS3Client(accessKey, accessSecret, region);
    }


    public async Task<GetObjectModel> GetObjectAsync(string name)
    {
        var request = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = name
        };

        var response = await _s3Client.GetObjectAsync(request);
        
        return new GetObjectModel
        {
            ContentType = response.Headers.ContentType,
            Content = response.ResponseStream
        };
    }

    public async Task<UploadObjectModel> UploadObjectAsync(IFormFile file)
    {
        byte[] fileBytes = new byte[file.Length];
        await file.OpenReadStream().ReadAsync(fileBytes, 0, (int)file.Length);

        var fileName = $"{Guid.NewGuid()}{file.FileName}";

        using var stream = new MemoryStream(fileBytes);
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = stream,
            ContentType = file.ContentType,
            CannedACL = S3CannedACL.PublicRead
        };

        var response = await _s3Client.PutObjectAsync(request);

        return new UploadObjectModel
        {
            Success = response.HttpStatusCode == System.Net.HttpStatusCode.OK,
            FileName = fileName
        };
    }

    public async Task<UploadObjectModel> RemoveObjectAsync(string fileName)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName
        };

        var response = await _s3Client.DeleteObjectAsync(request);

        return new UploadObjectModel
        {
            Success = response.HttpStatusCode == System.Net.HttpStatusCode.OK,
            FileName = fileName
        };
    }
}

