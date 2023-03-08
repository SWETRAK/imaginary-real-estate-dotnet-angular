using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ImaginaryRealEstate.Database;

public class OfferRepository: IOfferRepository
{
    private readonly ILogger<OfferRepository> _logger;
    private readonly IMongoCollection<Offer> _offersCollection;

    public OfferRepository(IMongoClient mongoClient, ILogger<OfferRepository> logger, MongoSettings settings)
    {
        _logger = logger;
        _offersCollection = mongoClient.GetDatabase(settings.DatabaseName)
            .GetCollection<Offer>(MongoConsts.OffersCollectionName);
    }

    public async Task<IEnumerable<Offer>> Get() =>
        await _offersCollection.Find(_ => true).ToListAsync();

    public async Task<Offer> GetById(ObjectId id) =>
        await _offersCollection.Find(offer => offer.Id == id).FirstOrDefaultAsync();

    public async Task<Offer> GetByIdAndAuthor(ObjectId offerId, ObjectId authorId) =>
        await _offersCollection.Find(offer => offer.Id == offerId && offer.AuthorId == authorId.ToString()).FirstOrDefaultAsync(); 

    public async Task<IEnumerable<Offer>> GetContainingAddress(string address) =>
        await _offersCollection.Find(offer => offer.Address.Contains(address)).ToListAsync();

    public async Task<IEnumerable<Offer>> GetManyByIds(IEnumerable<ObjectId> offersIds) =>
        await _offersCollection.Find(offer => offersIds.Contains(offer.Id)).ToListAsync();

    public async Task<IEnumerable<Offer>> GetManyByAuthorId(ObjectId authorId) =>
        await _offersCollection.Find(offer => offer.AuthorId == authorId.ToString()).ToListAsync();

    public async Task Insert(Offer offer) => 
        await _offersCollection.InsertOneAsync(offer);

    public async Task Remove(ObjectId id) =>
        await _offersCollection.DeleteOneAsync(offer => offer.Id == id);
    
    public async Task Update(Offer offer)
    {
        await _offersCollection.ReplaceOneAsync((oldOffer) => oldOffer.Id == offer.Id, offer);
    }
    
}