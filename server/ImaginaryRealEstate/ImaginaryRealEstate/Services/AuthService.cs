using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ImaginaryRealEstate.Authentication;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Auth;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ImaginaryRealEstate.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly DomainDbContext _dbContext;
    private readonly IMapper _mapper;

    public AuthService(
        ILogger<AuthService> logger, 
        IPasswordHasher<User> passwordHasher,
        AuthenticationSettings authenticationSettings,
        DomainDbContext dbContext,
        IMapper mapper
        )
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public UserInfoDto CreateUser(RegisterUserWithPasswordDto registerUserWithPasswordDto )
    {  
        var newUser = new User
        {
            Email = registerUserWithPasswordDto.Email,
            FirstName = registerUserWithPasswordDto.FirstName,
            LastName = registerUserWithPasswordDto.LastName,
            DateOfBirth = registerUserWithPasswordDto.DateOfBirth,
            Role = registerUserWithPasswordDto.Role
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, registerUserWithPasswordDto.Password);
        newUser.HashPassword = hashedPassword;

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
        _logger.LogInformation($"User created with {newUser.Email} at {DateTime.Now.ToString("yyyy-MMMM-dd h:mm:ss tt zz")}.");

        var userInfoDto = _mapper.Map<UserInfoDto>(newUser);
        userInfoDto.Token = GenerateJwtToken(newUser);

        _logger.LogInformation($"User {newUser.Email} logged in at {DateTime.Now.ToString("yyyy-MMMM-dd h:mm:ss tt zz")}.");
        return userInfoDto;
    }
    
    public UserInfoDto LoginUser(LoginUserWithPasswordDto userDto)
    {
        var user = _dbContext
            .Users
            .FirstOrDefault(u => u.Email == userDto.Email);

        if (user is null) throw new InvalidLoginDataException();

        var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, userDto.Password);
        if (result == PasswordVerificationResult.Failed) throw new InvalidLoginDataException();
        
        var userInfoDto = _mapper.Map<UserInfoDto>(user);
        userInfoDto.Token = GenerateJwtToken(user);
        _logger.LogInformation($"User created with {user.Email} at {DateTime.Now.ToString("yyyy-MMMM-dd h:mm:ss tt zz")}.");
        return userInfoDto;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),    
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"))
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
