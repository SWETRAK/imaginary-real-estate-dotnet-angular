using ImaginaryRealEstate.Models.Offers;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IOfferService
{
    IEnumerable<OfferResultDto> GetOffers();
    IEnumerable<OfferResultDto> GetOffersByAddress(string address);
    OfferResultDto GetOfferById(string identifier);
    OfferResultDto CreateOffer(NewOfferIncomingDto incomingDto, string userId);
    bool DeleteOffer(string offerId, string userId);
    bool LikeOffer(string offerId, string userId);
    bool UnLikeOffer(string offerId, string userId);
}