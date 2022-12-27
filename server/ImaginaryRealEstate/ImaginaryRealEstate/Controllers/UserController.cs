using ImaginaryRealEstate.Authentication;
using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Models.Offers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImaginaryRealEstate.Controllers;

[ApiController]
[Route("user")]
public class UserController: Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    [HttpGet("info")]
    public ActionResult<UserInfoDto> GetUserInfo()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);

        var result = new UserInfoDto();
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("liked")]
    public ActionResult<IEnumerable<OfferResultDto>> GetUserLiked()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);;

        var result = new List<OfferResultDto>();
        return Ok(result);
    }

    [Authorize(Roles = $"{Roles.Author},{Roles.Admin}")]
    [HttpGet("listed")]
    public ActionResult<IEnumerable<OfferResultDto>> GetListedOffers()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);

        var result = new List<OfferResultDto>();
        return Ok(result);
    }
}