using System.Net.Mime;
using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ImaginaryRealEstate.Database;

public class ImageRepository : IImageRepository
{
    private readonly ILogger<ImageRepository> _logger;
    private readonly IMongoCollection<Image> _imagesCollection;

    public ImageRepository(IMongoClient mongoClient, ILogger<ImageRepository> logger, MongoSettings settings)
    {
        _logger = logger;
        _imagesCollection = mongoClient.GetDatabase(settings.DatabaseName)
            .GetCollection<Image>(MongoConsts.ImagesCollectionName);
    }

    public async Task<Image> GetById(ObjectId imageId) =>
        await _imagesCollection.Find(image => image.Id == imageId).FirstOrDefaultAsync();

    public async Task<IEnumerable<Image>> GetManyByIds(IEnumerable<ObjectId> imageIds) =>
        await _imagesCollection.Find(image => imageIds.Contains(image.Id)).ToListAsync();
    
    public async Task Insert(Image image) =>
        await _imagesCollection.InsertOneAsync(image);
}