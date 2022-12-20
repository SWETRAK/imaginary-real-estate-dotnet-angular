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
            "AWS Access Key ",
            "AWS Secreet Access Key",
            RegionEndpoint.GetBySystemName("Your Region Name here")
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
    
    public async Task<PutObjectResponse> UploadObjectAsync(string bucketName, string key, string contentBody)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = key,
            ContentBody = contentBody
        };
        var response1 = await _s3Client.PutObjectAsync(putRequest);
        _logger.LogInformation($"{nameof(S3Service)}|{nameof(UploadObjectAsync)}|File: \"{key}\" was uploaded to bucket {bucketName}");
        return response1;
    }

    public async Task<PutObjectResponse> UploadObjectAsync(string bucketName, string key, string filePath, string contentType)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = key,
            FilePath = filePath,
            ContentType = contentType
        };
        var response1 = await _s3Client.PutObjectAsync(putRequest);
        _logger.LogInformation($"{nameof(S3Service)}|{nameof(UploadObjectAsync)}|File: \"{key}\", contentType \"{contentType}\" was uploaded to bucket {bucketName}");
        return response1;
    }

    public async Task UploadFileAsync(string bucketName, string filePath, string uniqueKey)
    {
        var fileTransferUtility = new TransferUtility(_s3Client);

        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
        {
            BucketName = bucketName,
            FilePath = filePath,
            StorageClass = S3StorageClass.StandardInfrequentAccess,
            PartSize = 6291456,
            Key = uniqueKey,
            CannedACL = S3CannedACL.PublicRead
        };
        
        //fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
        
        await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        _logger.LogInformation($"{nameof(S3Service)}|{nameof(UploadFileAsync)}|File: \"{filePath}\", as \"{uniqueKey}\" was uploaded to bucket {bucketName}");
    }
}