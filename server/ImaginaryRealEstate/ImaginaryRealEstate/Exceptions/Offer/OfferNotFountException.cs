using System.Runtime.Serialization;

namespace ImaginaryRealEstate.Exceptions.Offer;

public class OfferNotFountException: Exception
{
    public OfferNotFountException(): base("Offer with provided Guid not exists")
    {
    }

    protected OfferNotFountException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public OfferNotFountException(string message) : base(message)
    {
    }

    public OfferNotFountException(string message, Exception innerException) : base(message, innerException)
    {
    }
}