using AutoMapper;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Auth;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Models.Users;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper, 
        ILogger<UserService> logger, 
        IPasswordHasher<User> passwordHasher, 
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<UserInfoDto> ChangePassword(ChangePasswordDto changePasswordDto, string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObjectId)) throw new NoGuidException();
        
        var user = await _userRepository.GetById(userObjectId);
        if (user == null) throw new OfferNotFountException();
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, changePasswordDto.CurrentPassword);
        if (result == PasswordVerificationResult.Failed) throw new InvalidLoginDataException();

        user.HashPassword = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);
        _logger.LogInformation("User with id: {} changed password", userIdString);
        await _userRepository.Update(user);

        var userInfo = _mapper.Map<UserInfoDto>(user);
        return userInfo;
    }

    public async Task<UserInfoDto> GetUserInfo(string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObjectId)) throw new NoGuidException();

        var user = await _userRepository.GetById(userObjectId);

        if (user == null) throw new OfferNotFountException();
        
        var userInfo = _mapper.Map<UserInfoDto>(user);
        return userInfo;
    }

    public async Task<IEnumerable<OfferResultDto>> GetLikedOffers(string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObjectId)) throw new NoGuidException();

        var user = await _userRepository.GetById(userObjectId);

        // _dbContext
            // .Users
            // .Include(u => u.LikedOffers)
            // .ThenInclude(o => o.Images)
            // .Include(u => u.LikedOffers)
            // .ThenInclude(o => o.Author)
            // .FirstOrDefault(u => u.Id == userIdString);
            
        if (user == null) throw new OfferNotFountException();
    
        var result = _mapper.Map<IEnumerable<OfferResultDto>>(user.LikedOffers);
        return result;
    }
    
    public async Task<IEnumerable<OfferResultDto>> GetListedOffers(string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObjectId)) throw new NoGuidException();
        
        var offers = await _userRepository.GetById(userObjectId);
            // _dbContext
            // .Offers
            // .Include(o => o.Author)
            // .Include(o => o.Images)
            // .Where(o => o.AuthorId == userIdString)
            // .ToList();
            //
        var result = _mapper.Map<IEnumerable<OfferResultDto>>(offers);
        return result;
    }
}