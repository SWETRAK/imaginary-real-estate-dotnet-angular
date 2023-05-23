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
    private readonly IImageRepository _imageRepository;
    private readonly IOfferRepository _offerRepository;

    public UserService(
        IMapper mapper, 
        ILogger<UserService> logger, 
        IPasswordHasher<User> passwordHasher, 
        IUserRepository userRepository, IImageRepository imageRepository, IOfferRepository offerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _imageRepository = imageRepository;
        _offerRepository = offerRepository;
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

        if (user == null) throw new OfferNotFountException();
        
        var likedOffers =  new List<Offer>() {};

        if (user.LikedOffersIds is not null)
        {
            likedOffers =
                (await _offerRepository.GetManyByIds(user.LikedOffersIds.Select(id => ObjectId.Parse(id)).ToList()))
                .ToList();
            
            foreach (var offer in likedOffers)
            {
                offer.Images = await _imageRepository.GetManyByIds(offer.ImagesIds.Select(id => ObjectId.Parse(id)).ToList());
                if (offer.LikesIds is not null) offer.Likes = await _userRepository.GetManyByIds(offer.LikesIds.Select(id => ObjectId.Parse(id)).ToList());
                offer.Author = await _userRepository.GetById(ObjectId.Parse(offer.AuthorId));
            }
        }
        
        user.LikedOffers = likedOffers;

        return _mapper.Map<IEnumerable<OfferResultDto>>(user.LikedOffers);
    }
    
    public async Task<IEnumerable<OfferResultDto>> GetListedOffers(string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObjectId)) throw new NoGuidException();
        
        var offers = await _offerRepository.GetManyByAuthorId(userObjectId);

        foreach (var offer in offers)
        {
            offer.Images = await _imageRepository.GetManyByIds(offer.ImagesIds.Select(id => ObjectId.Parse(id)).ToList());
            if (offer.LikesIds is not null) offer.Likes = await _userRepository.GetManyByIds(offer.LikesIds.Select(id => ObjectId.Parse(id)).ToList());
            offer.Author = await _userRepository.GetById(ObjectId.Parse(offer.AuthorId));
        }

        var result = _mapper.Map<IEnumerable<OfferResultDto>>(offers);
        return result;
    }
}