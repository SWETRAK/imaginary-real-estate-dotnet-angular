namespace ImaginaryRealEstate.Exceptions.Auth;

public class InvalidLoginDataException: Exception
{
    public InvalidLoginDataException(): base("Invalid login or password")
    {
    }

    public InvalidLoginDataException(string message) : base(message)
    {
    }

    public InvalidLoginDataException(string message, Exception innerException) : base(message, innerException)
    {
    }
}