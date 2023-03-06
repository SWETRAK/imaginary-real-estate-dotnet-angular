using ImaginaryRealEstate.Models.Auth;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IAuthService
{
    Task<(UserInfoDto, string)> GetUserInfo(string guideId);
    Task<(UserInfoDto, string)> CreateUser(RegisterUserWithPasswordDto registerUserWithPasswordDto);
    Task<(UserInfoDto, string)> LoginUser(LoginUserWithPasswordDto userDto);
}