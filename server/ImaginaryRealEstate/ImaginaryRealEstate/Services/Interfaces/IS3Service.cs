using Amazon.S3.Model;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IS3Service
{
    Task CreateNewBucketAsync(string bucketName);
    void UploadFile(string bucketName, MemoryStream fileStream, string contentType, string uniqueKey);
    Task<(MemoryStream, string)> GetFile(string bucketName, string uniqueKey);
}