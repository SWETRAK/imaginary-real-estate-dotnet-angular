using ImaginaryRealEstate.Authentication;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImaginaryRealEstate.Controllers;

[ApiController]
[Route("auth")]
public class AuthController: Controller
{
    private readonly IAuthService _authService;
    private readonly AuthenticationSettings _authenticationSettings;

    public AuthController(IAuthService authService, AuthenticationSettings authenticationSettings)
    {
        _authService = authService;
        _authenticationSettings = authenticationSettings;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserInfoDto>> CheckUserResult()
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var loginResult = await _authService.GetUserInfo(userId);
        
        Response.Cookies.Append(
            "X-Access-Token", 
            loginResult.Item2, 
            new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays)
            });
        
        return Ok(loginResult.Item1);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserInfoDto>> LoginUserWithPassword([FromBody] LoginUserWithPasswordDto loginDto)
    {
        var loginResult = await _authService.LoginUser(loginDto);
        
        Response.Cookies.Append(
            "X-Access-Token", 
            loginResult.Item2, 
            new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays)
            });
        return Ok(loginResult.Item1);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserInfoDto>> RegisterUserWithPassword([FromBody] RegisterUserWithPasswordDto registerDto)
    {
        var registerResult = await _authService.CreateUser(registerDto);
        
        Response.Cookies.Append(
            "X-Access-Token", 
            registerResult.Item2,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays)
            });
        return Created("", registerResult.Item1);
    }

    [Authorize]
    [HttpDelete("logout")]
    public ActionResult<bool> LogoutUserWithPassword()
    {
        Response.Cookies.Delete("X-Access-Token");
        return Ok(true);
    }
}