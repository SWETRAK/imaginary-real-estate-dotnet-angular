using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImaginaryRealEstate.Entities;

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [BsonIgnore] [BsonExtraElements] public virtual List<Offer> LikedOffers { get; set; } = new List<Offer>();

    public List<string> LikedOffersIds { get; set; } = new List<string>() {};
    
    public string Role { get; set; }
}