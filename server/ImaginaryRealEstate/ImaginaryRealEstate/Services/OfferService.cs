using AutoMapper;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Exceptions.Offer;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Services.Interfaces;
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
    
    public IEnumerable<OfferResultDto> GetOffers()
    {
        var offerFromDatabase = _dbContext
            .Offers
            .Include(i => i.Images)
            .Include( i => i.Likes)
            .ToList();

        _logger.LogInformation("Someone gets all offers from database");
        return _mapper.Map<List<OfferResultDto>>(offerFromDatabase);
    }

    public IEnumerable<OfferResultDto> GetOffersByAddress(string address)
    {
        var offerFromDatabase = _dbContext
            .Offers
            .Include(i => i.Images)
            .Include( i => i.Likes)
            .Where(o => o.Address.Contains(address))
            .ToList();

        _logger.LogInformation("Someone gets all offers for address containing {} from database", address);
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
            .Include(i => i.Likes)
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
        var result = _mapper.Map<OfferResultDto>(offer);
        return result;
    }

    public bool DeleteOffer(string offerId, string userId)
    {
        if (!Guid.TryParse(offerId, out var offerIdGuid)) throw new NoGuidException();
        if (!Guid.TryParse(userId, out var userIdGuid)) throw new NoGuidException();

        var guid = _dbContext.Offers.FirstOrDefault(o => o.Author.Id == userIdGuid && o.Id == offerIdGuid);
        if (guid == null) throw new OfferNotFountException();

        _dbContext.Offers.Remove(guid);
        _dbContext.SaveChanges();
        return true;
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
        
        offer.Likes = offer.Likes.Append(user).ToList();
        user.LikedOffers = user.LikedOffers.Append(offer).ToList();

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