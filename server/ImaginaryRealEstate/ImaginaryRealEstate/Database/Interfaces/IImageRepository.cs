using ImaginaryRealEstate.Entities;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Database.Interfaces;

public interface IImageRepository
{
    Task<Image> GetById(ObjectId imageId);

    Task<IEnumerable<Image>> GetManyByIds(IEnumerable<ObjectId> imageIds);

    Task Insert(Image image);
}