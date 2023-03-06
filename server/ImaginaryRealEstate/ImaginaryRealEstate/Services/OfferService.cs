using AutoMapper;
using ImaginaryRealEstate.Database;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace ImaginaryRealEstate.Services;

public class OfferService : IOfferService
{

    private readonly ILogger<OfferService> _logger;
    private readonly IMapper _mapper;
    private readonly IOfferRepository _offerRepository;
    private readonly IUserRepository _userRepository;

    public OfferService (
        ILogger<OfferService> logger, 
        IMapper mapper, 
        IOfferRepository offerRepository, 
        IUserRepository userRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _offerRepository = offerRepository;
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<OfferResultDto>> GetOffers()
    {
        var offerFromDatabase = await _offerRepository.Get();
            
        
        // TODO: Get values for images and likes 
            // _dbContext
            // .Offers
            // .Include(i => i.Images)
            // .Include( i => i.Likes)
            // .ToList();

        _logger.LogInformation("Someone gets all offers from database");
        return _mapper.Map<List<OfferResultDto>>(offerFromDatabase);
    }

    public async Task<IEnumerable<OfferResultDto>> GetOffersByAddress(string addressString)
    {
        var offerFromDatabase = await _offerRepository.GetContainingAddress(addressString);
            
            //
            // _dbContext
            // .Offers
            // .Include(i => i.Images)
            // .Include( i => i.Likes)
            // .Where(o => o.Address.Contains(address))
            // .ToList();

        _logger.LogInformation("Someone gets all offers for address containing {} from database", addressString);
        return _mapper.Map<List<OfferResultDto>>(offerFromDatabase);
    }

    public async Task<OfferResultDto> GetOfferById(string idString)
    {
        if (!ObjectId.TryParse(idString, out var offerObjectId)) throw new NoGuidException();
        
        var offerById = await _offerRepository.GetById(offerObjectId);
        
        // _dbContext.Offers
            // .Include(i => i.Images)
            // .Include(i => i.Author)
            // .Include(i => i.Likes)
            // .FirstOrDefault(w => w.Id == idString);
        
        if (offerById == null) throw new OfferNotFountException();
        
        var result = _mapper.Map<OfferResultDto>(offerById);
        return result;
    }

    public async Task<OfferResultDto> CreateOffer(NewOfferIncomingDto incomingDto, string userIdString)
    {
        if (!ObjectId.TryParse(userIdString, out var userObejctId)) throw new NoGuidException();

        var user = await _userRepository.GetById(userObejctId);
        if (user == null) throw new OfferNotFountException();
        
        var offer = _mapper.Map<Offer>(incomingDto);
        offer.Author = user;
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

    // public bool LikeOffer(string offerIdString, string userIdString)
    // {
    //     var offer = _dbContext.Offers
    //         .Include(o => o.Likes)
    //         .FirstOrDefault(o => o.Id == offerIdString);
    //     
    //     var user = _dbContext.Users
    //         .Include(u => u.LikedOffers)
    //         .FirstOrDefault(u => u.Id == userIdString);
    //
    //
    //     if (offer is null) throw new OfferNotFountException();
    //     if (user is null) throw new OfferNotFountException();
    //     
    //     offer.Likes = offer.Likes.Append(user).ToList();
    //     user.LikedOffers = user.LikedOffers.Append(offer).ToList();
    //
    //     _dbContext.Offers.Update(offer);
    //     _dbContext.Users.Update(user);
    //
    //     _dbContext.SaveChanges();
    //     return true;
    // }
    //
    // public bool UnLikeOffer(string offerIdString, string userIdString)
    // {
    //     var offer = _dbContext.Offers
    //         .Include(o => o.Likes)
    //         .FirstOrDefault(o => o.Id == offerIdString);
    //     var user = _dbContext.Users
    //         .Include(u => u.LikedOffers)
    //         .FirstOrDefault(u => u.Id == userIdString);
    //
    //     if (offer is null) throw new OfferNotFountException();
    //     if (user is null) throw new OfferNotFountException();
    //
    //     offer.Likes = offer.Likes.Where(u => u.Id != user.Id).ToList();
    //     user.LikedOffers = user.LikedOffers.Where(o => o.Id != offer.Id).ToList();
    //
    //     _dbContext.Offers.Update(offer);
    //     _dbContext.Users.Update(user);
    //
    //     _dbContext.SaveChanges();
    //     return true;
    // }
}