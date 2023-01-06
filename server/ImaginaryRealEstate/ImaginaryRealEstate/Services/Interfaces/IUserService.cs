using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Models.Users;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IUserService
{
    UserInfoDto ChangePassword(ChangePasswordDto changePasswordDto, string userIdString);
    UserInfoDto GetUserInfo(string userIdString);
    IEnumerable<OfferResultDto> GetLikedOffers(string userIdString);
    IEnumerable<OfferResultDto> GetListedOffers(string userIdString);
}