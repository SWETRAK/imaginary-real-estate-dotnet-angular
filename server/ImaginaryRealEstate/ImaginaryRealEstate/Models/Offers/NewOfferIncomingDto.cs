namespace ImaginaryRealEstate.Models.Offers;

public class NewOfferIncomingDto
{
    public string Title { get; set; }
    public string Address { get; set; }

    public float Price { get; set; }
    public float Bedrooms { get; set; }
    public float Bathrooms { get; set; }
    public float Area { get; set; }

    public string Description { get; set; }
}