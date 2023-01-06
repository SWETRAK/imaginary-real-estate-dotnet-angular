using Microsoft.AspNetCore.Identity;

namespace ImaginaryRealEstate.Models.Users;

public class ChangePasswordDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepeatPassword { get; set; }
}