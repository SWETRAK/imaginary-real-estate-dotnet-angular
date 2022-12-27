using System.Runtime.Serialization;

namespace ImaginaryRealEstate.Exceptions.Offer;

public class NoGuidException: Exception
{
    public NoGuidException(): base("Incorrect Guid string")
    {
    }

    protected NoGuidException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoGuidException(string message) : base(message)
    {
    }

    public NoGuidException(string message, Exception innerException) : base(message, innerException)
    {
    }
}