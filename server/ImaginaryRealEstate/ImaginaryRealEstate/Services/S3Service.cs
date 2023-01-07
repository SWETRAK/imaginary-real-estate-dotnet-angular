using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using ImaginaryRealEstate.Services.Interfaces;
using ImaginaryRealEstate.Settings;

namespace ImaginaryRealEstate.Services;

/// <summary>
/// Amazon AWS S3 Service,
/// Can throw AmazonS3Exception  
/// </summary>
public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly ILogger<S3Service> _logger;
    private readonly AwsS3Setting _s3Setting;
    
    public S3Service(
        ILogger<S3Service> logger,
        AwsS3Setting awsS3Setting
        )
    {
        _logger = logger;
        _s3Setting = awsS3Setting;
        _s3Client = new AmazonS3Client(
            _s3Setting.AwsAccessKeyId,
            _s3Setting.AwsSecretAccessKey,
            RegionEndpoint.GetBySystemName(_s3Setting.AwsLocale)
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
            await _s3Client.PutBucketAsync(putBucketRequest);
            _logger.LogInformation("{S3ServiceName}|{NewBucketAsyncName}|Buket with name {BucketName} created ", nameof(S3Service), nameof(CreateNewBucketAsync), bucketName);
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

        _logger.LogInformation("{S3ServiceName}|{UploadFileName},Uploaded file as \"{UniqueKey}\" was uploaded to bucket {BucketName}", nameof(S3Service), nameof(UploadFile), uniqueKey, bucketName);
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
        
        var memoryStream = new MemoryStream();
        await using var responseStream = response.ResponseStream;
        await responseStream.CopyToAsync(memoryStream);
        
        return (memoryStream, response.Headers.ContentType);
    }
}