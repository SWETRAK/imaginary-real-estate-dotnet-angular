namespace ImaginaryRealEstate.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public virtual IEnumerable<Offer> LikedOffers { get; set; }

    public string Role { get; set; }
}