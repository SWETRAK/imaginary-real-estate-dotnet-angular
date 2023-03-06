using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImaginaryRealEstate.Entities;

public class Image
{
    public ObjectId Id { get; set; }

    public virtual Offer Offer { get; set; }

    public string FileName { get; set; }

    public bool IsFrontPhoto { get; set; }
}