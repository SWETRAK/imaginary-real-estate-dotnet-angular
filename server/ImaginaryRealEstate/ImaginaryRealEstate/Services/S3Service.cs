using System.Dynamic;
using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using ImaginaryRealEstate.Services.Interfaces;

namespace ImaginaryRealEstate.Services;

/// <summary>
/// Amazon AWS S3 Service,
/// Can throw AmazonS3Exception  
/// </summary>
public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly ILogger<S3Service> _logger;
    
    public S3Service(ILogger<S3Service> logger)
    {
        _logger = logger;
        _s3Client = new AmazonS3Client(
            "AKIATIW5P454BGIVVDLH",
            "B4Y5rrFCem5AJup6wm9gFkLkhQeCZbeAeSnviaiD",
            RegionEndpoint.GetBySystemName("eu-west-3")
        );
    }

    public async Task CreateNewBucketAsync(string bucketName)
    {
        if (!(await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName)))
        {
            var putBucketRequest = new PutBucketRequest
            {
                BucketName = bucketName,
                UseClientRegion = true
            };
            var putBucketResponse = await _s3Client.PutBucketAsync(putBucketRequest);
            _logger.LogInformation($"{nameof(S3Service)}|{nameof(CreateNewBucketAsync)}|Buket with name {bucketName} created.");
        }
    }

    public void UploadFile(string bucketName, MemoryStream fileStream, string contentType, string uniqueKey)
    {
        var fileTransferUtility = new TransferUtility(_s3Client);

        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
        {
            BucketName = bucketName,
            StorageClass = S3StorageClass.StandardInfrequentAccess,
            InputStream = fileStream,
            PartSize = 6291456,
            Key = uniqueKey,
            ContentType = contentType
        };
        
        fileTransferUtility.Upload(fileTransferUtilityRequest);

        _logger.LogInformation($"{nameof(S3Service)}|{nameof(UploadFile)},Uploaded file as \"{uniqueKey}\" was uploaded to bucket {bucketName}");
    }

    public async Task<(MemoryStream, string)> GetFile(string bucketName, string uniqueKey)
    {

        var fileTransferUtility = new TransferUtility(_s3Client);
        
        var getRequest = new GetObjectRequest
        {
            BucketName = bucketName,
            Key = uniqueKey,
        };
        
        using var response = await fileTransferUtility.S3Client.GetObjectAsync(getRequest);
        if (response.ResponseStream == null) throw new Exception("File not found");
        
        // Convert MD5Stream to MemoryStream 
        var memoryStream = new MemoryStream();
        await using var responseStream = response.ResponseStream;
        await responseStream.CopyToAsync(memoryStream);
        
        return (memoryStream, response.Headers.ContentType);
    }
}