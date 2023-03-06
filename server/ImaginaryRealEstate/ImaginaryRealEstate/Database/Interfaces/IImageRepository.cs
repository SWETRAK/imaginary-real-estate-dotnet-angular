using ImaginaryRealEstate.Entities;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Database.Interfaces;

public interface IImageRepository
{
    Task<Image> GetById(ObjectId imageId);

    Task Insert(Image image);
}