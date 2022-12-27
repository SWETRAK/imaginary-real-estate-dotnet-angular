using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Models.Images;

namespace ImaginaryRealEstate.Models.Offers;

public class OfferResultDto
{
    public string Identifier { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }
    
    public MinimalUserInfo Author { get; set; }
    
    public float Price { get; set; }
    public float Bedrooms { get; set; }
    public float Bathrooms { get; set; }
    public float Area { get; set; }

    public string Description { get; set; }
    
    public virtual IEnumerable<ImageOfferResultDto> Images { get; set; }
}