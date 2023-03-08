using AutoMapper;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Services;

public class OfferService : IOfferService
{

    private readonly ILogger<OfferService> _logger;
    private readonly IMapper _mapper;
    private readonly IOfferRepository _offerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IImageRepository _imageRepository;

    public OfferService (
        ILogger<OfferService> logger, 
        IMapper mapper, 
        IOfferRepository offerRepository, 
        IUserRepository userRepository,
        IImageRepository imageRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _offerRepository = offerRepository;
        _userRepository = userRepository;
        _imageRepository = imageRepository;
    }
    
    public async Task<IEnumerable<OfferResultDto>> GetOffers()
    {
        var offerFromDatabase = await _offerRepository.Get();

        foreach (var offer in offerFromDatabase)
        {
            offer.Images = await _imageRepository.GetManyByIds(offer.ImagesIds.Select(id => ObjectId.Parse(id)).ToList());
            if (offer.LikesIds is not null)
                offer.Likes = await _userRepository.GetManyByIds(offer.LikesIds.Select(id => ObjectId.Parse(id)).ToList());
            offer.Author = await _userRepository.GetById(ObjectId.Parse(offer.AuthorId));
        }

        _logger.LogInformation("Someone gets all offers from database");
        var result = _mapper.Map<List<OfferResultDto>>(offerFromDatabase);
        return result;
    }

    public async Task<IEnumerable<OfferResultDto>> GetOffersByAddress(string addressString)
    {
        var offerFromDatabase = await _offerRepository.GetContainingAddress(addressString);
            
        foreach (var offer in offerFromDatabase)
        {
            offer.Images = await _imageRepository.GetManyByIds(offer.ImagesIds.Select(id => ObjectId.Parse(id)).ToList());
            if (offer.LikesIds is not null) offer.Likes = await _userRepository.GetManyByIds(offer.LikesIds.Select(id => ObjectId.Parse(id)).ToList());
            offer.Author = await _userRepository.GetById(ObjectId.Parse(offer.AuthorId));
        }
        
        _logger.LogInformation("Someone gets all offers for address containing {} from database", addressString);
        return _mapper.Map<List<OfferResultDto>>(offerFromDatabase);
    }

    public async Task<OfferResultDto> GetOfferById(string idString)
    {
        if (!ObjectId.TryParse(idString, out var offerObjectId)) throw new NoGuidException();
        
        var offer = await _offerRepository.GetById(offerObjectId);
        offer.Images = await _imageRepository.GetManyByIds(offer.ImagesIds.Select(id => ObjectId.Parse(id)).ToList());
        if (offer.LikesIds is not null) offer.Likes = await _userRepository.GetManyByIds(offer.LikesIds.Select(id => ObjectId.Parse(id)).ToList());
        offer.Author = await _userRepository.GetById(ObjectId.Parse(offer.AuthorId));
        
        if (offer == null) throw new OfferNotFountException();
        
        var result = _mapper.Map<OfferResultDto>(offer);
        return result;
    }

    public async Task<OfferResultDto> CreateOffer(NewOfferIncomingDto incomingDto, string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObejctId)) throw new NoGuidException();

        var user = await _userRepository.GetById(userObejctId);
        if (user == null) throw new OfferNotFountException();
        
        var offer = _mapper.Map<Offer>(incomingDto);
        offer.AuthorId = user.Id.ToString();
        await _offerRepository.Insert(offer);
        var result = _mapper.Map<OfferResultDto>(offer);
        return result;
    }

    public async Task<bool> DeleteOffer(string offerIdString, string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObejctId)) throw new NoGuidException();
        if (!ObjectId.TryParse(offerIdString, out var offerObjectId)) throw new NoGuidException();

        var offer = await _offerRepository.GetByIdAndAuthor(offerObjectId, userObejctId);
        if (offer == null) throw new OfferNotFountException();

        await _offerRepository.Remove(offer.Id);
        return true;
    }

    public async Task<bool> LikeOffer(string offerIdString, string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObejctId)) throw new NoGuidException();
        if (!ObjectId.TryParse(offerIdString, out var offerObjectId)) throw new NoGuidException();
        
        var offer = await _offerRepository.GetById(offerObjectId);
        var user = await _userRepository.GetById(userObejctId);

        if (offer is null) throw new OfferNotFountException();
        if (user is null) throw new OfferNotFountException();
        
        offer.LikesIds.Add(user.Id.ToString());
        if (user.LikedOffersIds is null) user.LikedOffersIds = new List<string>();
        user.LikedOffersIds.Add(offer.Id.ToString());

        await _userRepository.Update(user);
        await _offerRepository.Update(offer);
        
        return true;
    }

    public async Task<bool> UnLikeOffer(string offerIdString, string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObejctId)) throw new NoGuidException();
        if (!ObjectId.TryParse(offerIdString, out var offerObjectId)) throw new NoGuidException();
        
        var offer = await _offerRepository.GetById(offerObjectId);
        var user = await _userRepository.GetById(userObejctId);
    
        if (offer is null) throw new OfferNotFountException();
        if (user is null) throw new OfferNotFountException();
    
        offer.LikesIds.Remove(user.Id.ToString());
        user.LikedOffersIds.Remove(offer.Id.ToString());
        
        await _userRepository.Update(user);
        await _offerRepository.Update(offer);

        return true;
    }
}