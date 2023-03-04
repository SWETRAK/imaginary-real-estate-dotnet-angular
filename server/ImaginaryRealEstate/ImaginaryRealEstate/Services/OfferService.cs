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

    public OfferResultDto GetOfferById(string idString)
    {
        var offerById = _dbContext.Offers
            .Include(i => i.Images)
            .Include(i => i.Author)
            .Include(i => i.Likes)
            .FirstOrDefault(w => w.Id == idString);
        
        if (offerById == null) throw new OfferNotFountException();
        
        var result = _mapper.Map<OfferResultDto>(offerById);
        return result;
    }

    public OfferResultDto CreateOffer(NewOfferIncomingDto incomingDto, string userIdString)
    {

        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userIdString);
        if (user == null) throw new OfferNotFountException();
        
        var offer = _mapper.Map<Offer>(incomingDto);
        offer.Author = user;
        _dbContext.Offers.Add(offer);
        _dbContext.SaveChanges();
        var result = _mapper.Map<OfferResultDto>(offer);
        return result;
    }

    public bool DeleteOffer(string offerIdString, string userIdString)
    {
        var guid = _dbContext.Offers.FirstOrDefault(o => o.Author.Id == userIdString && o.Id == offerIdString);
        if (guid == null) throw new OfferNotFountException();

        _dbContext.Offers.Remove(guid);
        _dbContext.SaveChanges();
        return true;
    }

    public bool LikeOffer(string offerIdString, string userIdString)
    {
        var offer = _dbContext.Offers
            .Include(o => o.Likes)
            .FirstOrDefault(o => o.Id == offerIdString);
        
        var user = _dbContext.Users
            .Include(u => u.LikedOffers)
            .FirstOrDefault(u => u.Id == userIdString);


        if (offer is null) throw new OfferNotFountException();
        if (user is null) throw new OfferNotFountException();
        
        offer.Likes = offer.Likes.Append(user).ToList();
        user.LikedOffers = user.LikedOffers.Append(offer).ToList();

        _dbContext.Offers.Update(offer);
        _dbContext.Users.Update(user);

        _dbContext.SaveChanges();
        return true;
    }
    
    public bool UnLikeOffer(string offerIdString, string userIdString)
    {
        var offer = _dbContext.Offers
            .Include(o => o.Likes)
            .FirstOrDefault(o => o.Id == offerIdString);
        var user = _dbContext.Users
            .Include(u => u.LikedOffers)
            .FirstOrDefault(u => u.Id == userIdString);

        if (offer is null) throw new OfferNotFountException();
        if (user is null) throw new OfferNotFountException();

        offer.Likes = offer.Likes.Where(u => u.Id != user.Id).ToList();
        user.LikedOffers = user.LikedOffers.Where(o => o.Id != offer.Id).ToList();

        _dbContext.Offers.Update(offer);
        _dbContext.Users.Update(user);

        _dbContext.SaveChanges();
        return true;
    }
}