using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Services.Interfaces;
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
    public UserInfoDto LoginUserWithPassword([FromBody] LoginUserWithPasswordDto loginDto)
    {
        return _authService.LoginUser(loginDto);
    }

    [HttpPost("register")]
    public UserInfoDto RegisterUserWithPassword([FromBody] RegisterUserWithPasswordDto registerDto)
    {
        return _authService.CreateUser(registerDto);
    }
}