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
    public async Task<ActionResult<UserInfoDto>> UpdatePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = await _userService.ChangePassword(changePasswordDto, userId);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("info")]
    public async Task<ActionResult<UserInfoDto>> GetUserInfo()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = await _userService.GetUserInfo(userId);
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("liked")]
    public async  Task<ActionResult<IEnumerable<OfferResultDto>>> GetUserLiked()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = await _userService.GetLikedOffers(userId);
        return Ok(result);
    }
    
    [Authorize(Roles = $"{Roles.Author},{Roles.Admin}")]
    [HttpGet("listed")]
    public async Task<ActionResult<IEnumerable<OfferResultDto>>> GetListedOffers()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = await _userService.GetListedOffers(userId);
        return Ok(result);
    }
}