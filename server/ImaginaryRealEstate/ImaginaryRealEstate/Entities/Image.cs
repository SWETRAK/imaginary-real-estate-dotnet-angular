using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImaginaryRealEstate.Entities;

[BsonIgnoreExtraElements]
public class Image
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string OfferId { get; set; }

    public string FileName { get; set; }

    public bool IsFrontPhoto { get; set; }
}