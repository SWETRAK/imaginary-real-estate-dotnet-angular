namespace ImaginaryRealEstate.Models.Auth;

public class RegisterUserWithPasswordDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Role { get; set; }
}