namespace ImaginaryRealEstate.Entities;

public class Offer
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }

    public Guid AuthorId { get; set; }
    public virtual User Author { get; set; }

    public float Price { get; set; }
    public float Bedrooms { get; set; }
    public float Bathrooms { get; set; }
    public float Area { get; set; }
    public string Description { get; set; }
    
    public virtual IEnumerable<User> Likes { get; set; }
    public virtual IEnumerable<Image> Images { get; set; }
}