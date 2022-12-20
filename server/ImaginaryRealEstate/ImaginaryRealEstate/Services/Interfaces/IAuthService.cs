using ImaginaryRealEstate.Models.Auth;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IAuthService
{
    UserInfoDto CreateUser(RegisterUserWithPasswordDto registerUserWithPasswordDto );
    UserInfoDto LoginUser(LoginUserWithPasswordDto userDto);
}