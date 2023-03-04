using ImaginaryRealEstate.Authentication;
using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Models.Users;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImaginaryRealEstate.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [Authorize]
    [HttpPut("update/password")]
    public ActionResult<UserInfoDto> UpdatePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = _userService.ChangePassword(changePasswordDto, userId);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("info")]
    public ActionResult<UserInfoDto> GetUserInfo()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = _userService.GetUserInfo(userId);
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("liked")]
    public ActionResult<IEnumerable<OfferResultDto>> GetUserLiked()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = _userService.GetLikedOffers(userId);
        return Ok(result);
    }
    
    [Authorize(Roles = $"{Roles.Author},{Roles.Admin}")]
    [HttpGet("listed")]
    public ActionResult<IEnumerable<OfferResultDto>> GetListedOffers()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
    
        var result = _userService.GetListedOffers(userId);
        return Ok(result);
    }
}