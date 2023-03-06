using ImaginaryRealEstate.Entities;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Database.Interfaces;

public interface IOfferRepository
{
    Task<IEnumerable<Offer>> Get();

    Task<Offer> GetById(ObjectId id);

    Task<Offer> GetByIdAndAuthor(ObjectId offerId, ObjectId authorId);

    Task<IEnumerable<Offer>> GetContainingAddress(string address);

    Task Insert(Offer offer);

    Task Remove(ObjectId id);
}