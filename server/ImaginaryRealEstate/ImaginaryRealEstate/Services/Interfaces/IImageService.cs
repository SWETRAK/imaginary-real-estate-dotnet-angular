using ImaginaryRealEstate.Models.Images;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IImageService
{
    ImageOfferResultDto CreateImage(IFormFile file, string offerId, bool frontPhoto);
    Task<(MemoryStream, string)> GetImage(string imageId);
}