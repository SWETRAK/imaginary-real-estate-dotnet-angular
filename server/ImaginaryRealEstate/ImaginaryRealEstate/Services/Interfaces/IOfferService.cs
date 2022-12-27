using ImaginaryRealEstate.Models.Offers;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IOfferService
{
    IEnumerable<OfferResultDto> GetOffers(OfferSearchIncomingDto searchIncomingDto);

    OfferResultDto GetOfferById(string identifier);

    OfferResultDto CreateOffer(NewOfferIncomingDto incomingDto, string userId);

    bool LikeOffer(string offerId, string userId);

    bool UnLikeOffer(string offerId, string userId);
}