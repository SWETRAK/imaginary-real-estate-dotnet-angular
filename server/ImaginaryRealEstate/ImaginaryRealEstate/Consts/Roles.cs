namespace ImaginaryRealEstate.Consts;

public static class Roles
{
    public const string Admin = "ADMIN";
    public const string User = "USER";

    public static string[] GetAllRoles()
    {
        return new[] { Admin, User };
    }
}