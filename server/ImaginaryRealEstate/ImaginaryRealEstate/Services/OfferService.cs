using AutoMapper;
using System.Linq;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImaginaryRealEstate.Services;

public class OfferService : IOfferService
{
    private readonly DomainDbContext _dbContext;
    private readonly ILogger<OfferService> _logger;
    private readonly IMapper _mapper;

    public OfferService(DomainDbContext dbContext, ILogger<OfferService> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }
    
    public IEnumerable<OfferResultDto> GetOffers(OfferSearchIncomingDto searchIncomingDto)
    {

        List<Offer> offerFromDatabase;
        if (searchIncomingDto.City.Equals(""))
        {
            offerFromDatabase = _dbContext.Offers.Include(i => i.Images).ToList();
        }
        else
        { 
            offerFromDatabase = _dbContext.Offers
                .Include(i => i.Images)
                .Where(w => w.Address.Contains(searchIncomingDto.City))
                .ToList();
        }
        
        // TODO: Add searching by parameters from `searchIncomingDto`
        // TODO: Implement mappings for offer and image
        // TODO: Add pagination to query result 
        _logger.LogInformation("Someone gets all offers from database");
        return _mapper.Map<List<OfferResultDto>>(offerFromDatabase);
    }
    
    public OfferResultDto GetOfferById(string identifier)
    {
        if (!Guid.TryParse(identifier, out var id))
        {
            _logger.LogError("Given identifier \"{}\" can not be parsed as Guid", identifier);
            throw new NoGuidException();
        }
        
        var offerById = _dbContext.Offers
            .Include(i => i.Images)
            .Include(i => i.Author)
            .FirstOrDefault(w => w.Id == id);
        
        if (offerById == null) throw new OfferNotFountException();
        
        var result = _mapper.Map<OfferResultDto>(offerById);
        return result;
    }

    public OfferResultDto CreateOffer(NewOfferIncomingDto incomingDto, string userId)
    {
        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            _logger.LogError("Given identifier \"{}\" can not be parsed as Guid", userId);
            throw new NoGuidException();
        }

        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userIdGuid);
        if (user == null) throw new OfferNotFountException();
        
        var offer = _mapper.Map<Offer>(incomingDto);
        offer.Author = user;
        _dbContext.Offers.Add(offer);
        _dbContext.SaveChanges();
        //offer = _dbContext.Offers.Include(o => o.Author).FirstOrDefault(o => o.Id == offer.Id);
        var result = _mapper.Map<OfferResultDto>(offer);
        return result;
    }

    public bool LikeOffer(string offerId, string userId)
    {
        if (!Guid.TryParse(offerId, out var offerIdGuid))
        {
            _logger.LogError("Given identifier \"{}\" can not be parsed as Guid", offerId);
            throw new NoGuidException();
        }
        
        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            _logger.LogError("Given identifier \"{}\" can not be parsed as Guid", userId);
            throw new NoGuidException();
        }

        var offer = _dbContext.Offers
            .Include(o => o.Likes)
            .FirstOrDefault(o => o.Id == offerIdGuid);
        var user = _dbContext.Users
            .Include(u => u.LikedOffers)
            .FirstOrDefault(u => u.Id == userIdGuid);

        if (offer == null) throw new OfferNotFountException();
        if (user == null) throw new OfferNotFountException();
        
        offer.Likes = offer.Likes.Append(user);
        user.LikedOffers = user.LikedOffers.Append(offer);

        _dbContext.Offers.Update(offer);
        _dbContext.Users.Update(user);

        _dbContext.SaveChanges();
        return true;
    }
    
    public bool UnLikeOffer(string offerId, string userId)
    {
        if (!Guid.TryParse(offerId, out var offerIdGuid))
        {
            _logger.LogError("Given identifier \"{}\" can not be parsed as Guid", offerId);
            throw new NoGuidException();
        }
        
        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            _logger.LogError("Given identifier \"{}\" can not be parsed as Guid", userId);
            throw new NoGuidException();
        }

        var offer = _dbContext.Offers
            .Include(o => o.Likes)
            .FirstOrDefault(o => o.Id == offerIdGuid);
        var user = _dbContext.Users
            .Include(u => u.LikedOffers)
            .FirstOrDefault(u => u.Id == userIdGuid);

        if (offer == null) throw new OfferNotFountException();
        if (user == null) throw new OfferNotFountException();

        offer.Likes = offer.Likes.Where(u => u.Id != user.Id).ToList();
        user.LikedOffers = user.LikedOffers.Where(o => o.Id != offer.Id).ToList();

        _dbContext.Offers.Update(offer);
        _dbContext.Users.Update(user);

        _dbContext.SaveChanges();
        return true;
    }
    
}