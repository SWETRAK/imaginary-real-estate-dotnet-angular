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
    private readonly IMinioService _minioService;

    public ImageService( 
        DomainDbContext dbContext, 
        ILogger<ImageService> logger, 
        IMapper mapper, 
        IMinioService minioService,
        MinioSetting minioSetting
        )
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _minioService = minioService;
    }

    public async Task<ImageOfferResultDto> CreateImage(IFormFile file, string offerId, bool frontPhoto)
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

        await using var stream = file.OpenReadStream();
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        await _minioService.InsertFile(imageEntity.FileName, contentType, memoryStream);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Photo with {} id and {} aws key was uploaded", imageEntity.Id, imageEntity.FileName);
        
        var result = _mapper.Map<ImageOfferResultDto>(imageEntity);
        return result;
    }

    public async Task<(MemoryStream, string)> GetImage(string imageId)
    {
        if (!Guid.TryParse(imageId, out var imageIdGuid)) throw new NoGuidException();

        var image = _dbContext.Images.FirstOrDefault(i => i.Id == imageIdGuid);
        if (image == null) throw new OfferNotFountException();
        this._logger.LogInformation("Photo with {} id was downloaded", image.Id);

        var response = await _minioService.GetFile(image.FileName);
        return response;
    }
}