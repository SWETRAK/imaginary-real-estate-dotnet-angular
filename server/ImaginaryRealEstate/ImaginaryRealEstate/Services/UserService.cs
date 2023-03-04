using AutoMapper;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Auth;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Models.Users;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using MongoFramework.Linq;
using Microsoft.EntityFrameworkCore;

namespace ImaginaryRealEstate.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly DomainDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IMapper mapper, ILogger<UserService> logger, IPasswordHasher<User> passwordHasher, DomainDbContext dbContext)
    {
        _mapper = mapper;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _dbContext = dbContext;
    }

    public UserInfoDto ChangePassword(ChangePasswordDto changePasswordDto, string userIdString)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userIdString);
        if (user == null) throw new OfferNotFountException();
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, changePasswordDto.CurrentPassword);
        if (result == PasswordVerificationResult.Failed) throw new InvalidLoginDataException();

        user.HashPassword = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);
        _logger.LogInformation("User with id: {} changed password", userIdString);
        _dbContext.Users.Update(user);

        var userInfo = _mapper.Map<UserInfoDto>(user);
        return userInfo;
    }

    public UserInfoDto GetUserInfo(string userIdString)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userIdString);
        if (user == null) throw new OfferNotFountException();
        
        var userInfo = _mapper.Map<UserInfoDto>(user);
        return userInfo;
    }

    public IEnumerable<OfferResultDto> GetLikedOffers(string userIdString)
    {
        var user = _dbContext
            .Users
            .Include(u => u.LikedOffers)
            .ThenInclude(o => o.Images)
            .Include(u => u.LikedOffers)
            .ThenInclude(o => o.Author)
            .FirstOrDefault(u => u.Id == userIdString);
        if (user == null) throw new OfferNotFountException();
    
        var result = _mapper.Map<IEnumerable<OfferResultDto>>(user.LikedOffers);
        return result;
    }
    
    public IEnumerable<OfferResultDto> GetListedOffers(string userIdString)
    {
        var offers = _dbContext
            .Offers
            .Include(o => o.Author)
            .Include(o => o.Images)
            .Where(o => o.AuthorId == userIdString)
            .ToList();
        
        var result = _mapper.Map<IEnumerable<OfferResultDto>>(offers);
        return result;
    }
}