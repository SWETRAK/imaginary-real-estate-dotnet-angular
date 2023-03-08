using Microsoft.VisualBasic.CompilerServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImaginaryRealEstate.Entities;

[BsonIgnoreExtraElements]
public class Offer
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }

    [BsonIgnore]
    public virtual User Author { get; set; }
    public string AuthorId { get; set; }

    public float Price { get; set; }
    public float Bedrooms { get; set; }
    public float Bathrooms { get; set; }
    public float Area { get; set; }
    public string Description { get; set; }

    [BsonIgnore] 
    public virtual IEnumerable<User> Likes { get; set; } = new List<User>();

    public List<string> LikesIds { get; set; } = new List<string>();

    [BsonIgnore] 
    public virtual IEnumerable<Image> Images { get; set; } = new List<Image>();

    public List<string> ImagesIds { get; set; } = new List<string>();
}