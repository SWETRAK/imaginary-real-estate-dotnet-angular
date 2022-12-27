namespace ImaginaryRealEstate.Models.Offers;

public class OfferSearchIncomingDto
{
    public string City { get; set; }
    public FloatRangeDto Bathrooms { get; set; }
    public FloatRangeDto Bedrooms { get; set; }
    public FloatRangeDto Size { get; set; }
}