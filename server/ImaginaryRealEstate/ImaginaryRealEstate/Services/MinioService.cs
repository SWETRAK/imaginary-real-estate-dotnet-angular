using ImaginaryRealEstate.Services.Interfaces;
using ImaginaryRealEstate.Settings;
using Minio;

namespace ImaginaryRealEstate.Services;

public class MinioService: IMinioService
{
    private readonly ILogger<MinioService> _logger;
    private readonly MinioClient _minioClient;
    private readonly MinioSetting _minioConfiguration;
    
    public MinioService(ILogger<MinioService> logger, MinioSetting minioConfiguration)
    {
        _logger = logger;
        _minioConfiguration = minioConfiguration;
        _minioClient = new MinioClient()
            .WithEndpoint(_minioConfiguration.Host, _minioConfiguration.Port)
            .WithCredentials(_minioConfiguration.AccessKey, _minioConfiguration.SecureKey)
            .Build();
    }

    public async Task InsertFile(string objectName, string contentType, MemoryStream fileStream)
    {
        // Go back to begin position of MemoryStream to save all data to in MinIO bucket
        fileStream.Seek(0, SeekOrigin.Begin);
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_minioConfiguration.BucketName)
            .WithObject(objectName)
            .WithContentType(contentType)
            .WithObjectSize(fileStream.Length)
            .WithStreamData(fileStream);

        await _minioClient.PutObjectAsync(putObjectArgs);
        
    }

    public async Task<(MemoryStream, string)> GetFile(string objectName)
    {
        var resultStream = new MemoryStream();
        var getObjectArgs = new GetObjectArgs()
            .WithBucket(_minioConfiguration.BucketName)
            .WithObject(objectName)
            .WithCallbackStream(stream => stream.CopyTo(resultStream));
        var result = await _minioClient.GetObjectAsync(getObjectArgs);
        return (resultStream, result.ContentType);
    }

    public async Task RemoveFile(string objectName)
    {
        var removeOjectArgs = new RemoveObjectArgs()
            .WithBucket(_minioConfiguration.BucketName)
            .WithObject(objectName);
        await _minioClient.RemoveObjectAsync(removeOjectArgs);
    }
}