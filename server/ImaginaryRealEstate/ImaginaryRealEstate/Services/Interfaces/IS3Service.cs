using Amazon.S3.Model;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IS3Service
{
    Task CreateNewBucketAsync(string bucketName);
    Task<PutObjectResponse> UploadObjectAsync(string bucketName, string key, string contentBody);
    Task<PutObjectResponse> UploadObjectAsync(string bucketName, string key, string filePath, string contentType);
    Task UploadFileAsync(string bucketName, string filePath, string uniqueKey);
}