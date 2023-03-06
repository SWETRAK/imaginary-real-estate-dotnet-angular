using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ImaginaryRealEstate.Authentication;
using ImaginaryRealEstate.Database;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Auth;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IMapper _mapper;

    private readonly IUserRepository _userRepository;

    public AuthService(
        ILogger<AuthService> logger, 
        IPasswordHasher<User> passwordHasher,
        AuthenticationSettings authenticationSettings,
        IMapper mapper, 
        IUserRepository userRepository)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<(UserInfoDto, string)> GetUserInfo(string guideId)
    {
        if (!ObjectId.TryParse(guideId, out var guideObjectId)) throw new NoGuidException();
        var user = await _userRepository.GetById(guideObjectId); 
        if (user is null) throw new InvalidLoginDataException();
        
        var token = GenerateJwtToken(user);
        var userInfoDto = _mapper.Map<UserInfoDto>(user);

        return (userInfoDto, token);
    }

    public async Task<(UserInfoDto, string)> CreateUser(RegisterUserWithPasswordDto registerUserWithPasswordDto )
    {
        var newUser = _mapper.Map<User>(registerUserWithPasswordDto);

        var hashedPassword = _passwordHasher.HashPassword(newUser, registerUserWithPasswordDto.Password);
        newUser.HashPassword = hashedPassword;

        await _userRepository.Insert(newUser);
        _logger.LogInformation("User created with {NewUserEmail} at {S}", newUser.Email, DateTime.Now.ToString("yyyy-MMMM-dd h:mm:ss tt zz"));

        var userInfoDto = _mapper.Map<UserInfoDto>(newUser);
        var token = GenerateJwtToken(newUser);

        _logger.LogInformation("User {NewUserEmail} logged in at {S}", newUser.Email, DateTime.Now.ToString("yyyy-MMMM-dd h:mm:ss tt zz"));
        return (userInfoDto, token);
    }
    
    public async Task<(UserInfoDto, string)> LoginUser(LoginUserWithPasswordDto userDto)
    {
        var user = await _userRepository.GetByEmail(userDto.Email);

        if (user is null) throw new InvalidLoginDataException();

        var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, userDto.Password);
        if (result == PasswordVerificationResult.Failed) throw new InvalidLoginDataException();
        
        var userInfoDto = _mapper.Map<UserInfoDto>(user);
        var token = GenerateJwtToken(user);
        _logger.LogInformation("User created with {UserEmail} at {S}", user.Email, DateTime.Now.ToString("yyyy-MMMM-dd h:mm:ss tt zz"));
        return (userInfoDto, token);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),    
            new(ClaimTypes.Role, user.Role),
            new("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(
            _authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
