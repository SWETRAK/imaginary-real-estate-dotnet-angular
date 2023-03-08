using AutoMapper;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Images;
using ImaginaryRealEstate.Services.Interfaces;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Services;

public class ImageService: IImageService
{
    private readonly ILogger<ImageService> _logger;
    private readonly IMapper _mapper;
    private readonly IMinioService _minioService;
    private readonly IOfferRepository _offerRepository;
    private readonly IImageRepository _imageRepository;
    
    public ImageService( 
        ILogger<ImageService> logger, 
        IMapper mapper, 
        IMinioService minioService, 
        IOfferRepository offerRepository, 
        IImageRepository imageRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _minioService = minioService;
        _offerRepository = offerRepository;
        _imageRepository = imageRepository;
    }

    public async Task<ImageOfferResultDto> CreateImage(IFormFile file, string offerId, bool frontPhoto)
    {
        if (!ObjectId.TryParse(offerId, out var offerObjectId)) throw new NoGuidException();
        var offer = await _offerRepository.GetById(offerObjectId);
        
        if (offer == null) throw new OfferNotFountException(); 
        
        var imageEntity = new Image
        {
            FileName = Guid.NewGuid().ToString(),
            IsFrontPhoto = frontPhoto,
            OfferId = offer.Id.ToString()
        };

        await _imageRepository.Insert(imageEntity);
        
        Console.WriteLine(imageEntity.Id);
        
        offer.ImagesIds.Add(imageEntity.Id.ToString());
        await _offerRepository.Update(offer);

        var contentType = file.ContentType;
        Console.Write(contentType);

        await using var stream = file.OpenReadStream();
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        await _minioService.InsertFile(imageEntity.FileName, contentType, memoryStream);
        _logger.LogInformation("Photo with {} id and {} aws key was uploaded", imageEntity.Id, imageEntity.FileName);
        
        var result = _mapper.Map<ImageOfferResultDto>(imageEntity);
        return result;
    }

    public async Task<(MemoryStream, string)> GetImage(string imageId)
    {
        if (!ObjectId.TryParse(imageId, out var imageObjectId)) throw new NoGuidException();

        var image = await _imageRepository.GetById(imageObjectId);

        if (image == null) throw new OfferNotFountException();
        _logger.LogInformation("Photo with {} id was downloaded", image.Id);

        var response = await _minioService.GetFile(image.FileName);
        return response;
    }
}