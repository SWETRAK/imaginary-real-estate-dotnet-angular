using ImaginaryRealEstate.Models.Images;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IImageService
{
    Task<ImageOfferResultDto> CreateImage(IFormFile file, string offerId, bool frontPhoto);
    Task<(MemoryStream, string)> GetImage(string imageId);
}