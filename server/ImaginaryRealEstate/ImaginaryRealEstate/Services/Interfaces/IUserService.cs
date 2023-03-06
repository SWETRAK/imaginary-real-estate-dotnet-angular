using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Models.Users;

namespace ImaginaryRealEstate.Services.Interfaces;

public interface IUserService
{
    Task<UserInfoDto> ChangePassword(ChangePasswordDto changePasswordDto, string userIdString);
    Task<UserInfoDto> GetUserInfo(string userIdString);
    Task<IEnumerable<OfferResultDto>> GetLikedOffers(string userIdString);
    Task<IEnumerable<OfferResultDto>> GetListedOffers(string userIdString);
}