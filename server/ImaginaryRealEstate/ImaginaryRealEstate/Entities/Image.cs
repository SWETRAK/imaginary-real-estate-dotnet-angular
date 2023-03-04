namespace ImaginaryRealEstate.Entities;

public class Image
{
    public string Id { get; set; }
    
    public Offer Offer { get; set; }

    public string FileName { get; set; }

    public bool IsFrontPhoto { get; set; }
}