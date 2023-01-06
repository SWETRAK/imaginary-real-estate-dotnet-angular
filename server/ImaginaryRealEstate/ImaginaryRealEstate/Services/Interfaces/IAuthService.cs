using ImaginaryRealEstate.Models.Auth;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IAuthService
{
    (UserInfoDto, string) GetUserInfo( string guideId );
    (UserInfoDto, string) CreateUser(RegisterUserWithPasswordDto registerUserWithPasswordDto );
    (UserInfoDto, string) LoginUser(LoginUserWithPasswordDto userDto);
}