namespace ImaginaryRealEstate.Entities;

public class Image
{
    public Guid Id { get; set; }
    
    public Guid OfferId { get; set; }
    public virtual Offer Offer { get; set; }

    public string FileName { get; set; }
    public string Url { get; set; }

    public bool IsFrontPhoto { get; set; }
}