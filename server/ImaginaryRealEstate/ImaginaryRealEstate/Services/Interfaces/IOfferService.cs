using ImaginaryRealEstate.Models.Offers;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IOfferService
{
    Task<IEnumerable<OfferResultDto>> GetOffers();
    Task<IEnumerable<OfferResultDto>> GetOffersByAddress(string addressString);
    Task<OfferResultDto> GetOfferById(string idString);
    Task<OfferResultDto> CreateOffer(NewOfferIncomingDto incomingDto, string userIdString);
    Task<bool> DeleteOffer(string offerIdString, string userIdString);
    // bool LikeOffer(string offerId, string userId);
    // bool UnLikeOffer(string offerId, string userId);
}