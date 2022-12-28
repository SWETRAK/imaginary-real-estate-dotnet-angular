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

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult<UserInfoDto> LoginUserWithPassword([FromBody] LoginUserWithPasswordDto loginDto)
    {
        var loginResult = _authService.LoginUser(loginDto);
        
        Response.Cookies.Append(
            "X-Access-Token", 
            loginResult.Token, 
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

        return Ok(loginResult);
    }

    [HttpPost("register")]
    public ActionResult<UserInfoDto> RegisterUserWithPassword([FromBody] RegisterUserWithPasswordDto registerDto)
    {
        var registerResult = _authService.CreateUser(registerDto);
        return Created("", registerResult);
    }

    [Authorize]
    [HttpDelete("logout")]
    public ActionResult<string> LogoutUserWithPassword()
    {
        Response.Cookies.Delete("X-Access-Token");
        return Ok();
    }
}