using AutoMapper;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Images;
using ImaginaryRealEstate.Services.Interfaces;
using ImaginaryRealEstate.Settings;

namespace ImaginaryRealEstate.Services;

public class ImageService: IImageService
{
    private readonly DomainDbContext _dbContext;
    private readonly ILogger<ImageService> _logger;
    private readonly IMapper _mapper;
    private readonly IS3Service _s3Service;
    private readonly AwsS3Setting _s3Setting;

    public ImageService(
        DomainDbContext dbContext, 
        ILogger<ImageService> logger, 
        IMapper mapper, 
        IS3Service s3Service,
        AwsS3Setting s3Settings
        )
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _s3Service = s3Service;
        _s3Setting = s3Settings;
    }

    public ImageOfferResultDto CreateImage(IFormFile file, string offerId, bool frontPhoto)
    {
        Console.WriteLine(offerId);
        if (!Guid.TryParse(offerId, out var offerIdGuid)) throw new NoGuidException();

        var offer = _dbContext.Offers.FirstOrDefault(o => o.Id == offerIdGuid);
        if (offer == null) throw new OfferNotFountException(); 
        
        var imageEntity = new Image
        {
            FileName = Guid.NewGuid().ToString(),
            IsFrontPhoto = frontPhoto,
            Offer = offer
        };
         
        _dbContext.Images.Add(imageEntity);

        var contentType = file.ContentType;
        Console.Write(contentType);

        using var stream = file.OpenReadStream();
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        _s3Service.UploadFile(_s3Setting.AwsBucketName, memoryStream, contentType, imageEntity.FileName);
        _dbContext.SaveChanges();
        
        var result = _mapper.Map<ImageOfferResultDto>(imageEntity);
        return result;
    }

    public async Task<(MemoryStream, string)> GetImage(string imageId)
    {
        if (!Guid.TryParse(imageId, out var imageIdGuid)) throw new NoGuidException();

        var image = _dbContext.Images.FirstOrDefault(i => i.Id == imageIdGuid);
        if (image == null) throw new OfferNotFountException();

        var response = await _s3Service.GetFile(_s3Setting.AwsBucketName, image.FileName);
        return response;
    }
}