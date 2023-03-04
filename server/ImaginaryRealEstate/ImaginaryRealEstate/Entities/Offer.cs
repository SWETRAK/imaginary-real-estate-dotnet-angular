namespace ImaginaryRealEstate.Entities;

public class Offer
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }

    public string AuthorId { get; set; }
    public User Author { get; set; }

    public float Price { get; set; }
    public float Bedrooms { get; set; }
    public float Bathrooms { get; set; }
    public float Area { get; set; }
    public string Description { get; set; }
    
    public IEnumerable<User> Likes { get; set; }
    public IEnumerable<Image> Images { get; set; }
}