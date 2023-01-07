using System.Diagnostics.Contracts;

namespace ImaginaryRealEstate.Settings;

public class AwsS3Setting
{
    public string AwsAccessKeyId { get; set; }
    public string AwsSecretAccessKey { get; set; }
    public string AwsLocale { get; set; }
    public string AwsBucketName { get; set; }
}