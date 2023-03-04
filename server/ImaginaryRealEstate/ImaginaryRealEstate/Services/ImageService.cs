using AutoMapper;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Images;
using ImaginaryRealEstate.Services.Interfaces;

namespace ImaginaryRealEstate.Services;

public class ImageService: IImageService
{
    private readonly DomainDbContext _dbContext;
    private readonly ILogger<ImageService> _logger;
    private readonly IMapper _mapper;
    private readonly IMinioService _s3Service;
    
    public ImageService( 
        DomainDbContext dbContext, 
        ILogger<ImageService> logger, 
        IMapper mapper, 
        IMinioService s3Service
    )
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _s3Service = s3Service;
    }

    public ImageOfferResultDto CreateImage(IFormFile file, string offerId, bool frontPhoto)
    {
        var offer = _dbContext.Offers.FirstOrDefault(o => o.Id == offerId);
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
        _s3Service.InsertFile(imageEntity.FileName, contentType, memoryStream);
        _logger.LogInformation("Photo with {} id and {} aws key was uploaded", imageEntity.Id, imageEntity.FileName);
        
        var result = _mapper.Map<ImageOfferResultDto>(imageEntity);
        return result;
    }

    public async Task<(MemoryStream, string)> GetImage(string imageId)
    {
        var image = _dbContext.Images.FirstOrDefault(i => i.Id == imageId);
        if (image == null) throw new OfferNotFountException();
        this._logger.LogInformation("Photo with {} id was downloaded", image.Id);

        var response = await _s3Service.GetFile(image.FileName);
        return response;
    }
}