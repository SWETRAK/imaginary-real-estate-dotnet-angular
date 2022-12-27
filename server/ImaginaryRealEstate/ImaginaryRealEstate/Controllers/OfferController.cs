using ImaginaryRealEstate.Authentication;
using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImaginaryRealEstate.Controllers;

[ApiController]
[Route("offers")]
public class OfferController : Controller
{
    private readonly IOfferService _offerService;
    private readonly ILogger<OfferController> _logger;

    public OfferController(IOfferService offerService, ILogger<OfferController> logger)
    {
        _offerService = offerService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OfferResultDto>> GetAllOffers([FromBody] OfferSearchIncomingDto searchIncomingDto)
    {
        var result = _offerService.GetOffers(searchIncomingDto);
        
        return Ok(result);
    }
    
    [HttpGet("{identifier}")]
    public ActionResult<OfferResultDto> GetById([FromRoute] string identifier)
    {
        var result = _offerService.GetOfferById(identifier);
        
        return Ok(result);
    }

    [Authorize(Roles = $"{Roles.Author},{Roles.Admin}")]
    [HttpPost]
    public ActionResult<OfferResultDto> Create([FromBody] NewOfferIncomingDto newOfferDto)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = _offerService.CreateOffer(newOfferDto, userId);
        
        return Created($"/offers/{result.Identifier}", result);
    }
    
    [Authorize]
    [HttpPost("{offerId}/like")]
    public ActionResult<bool> LikeOffer([FromRoute] string offerId)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = _offerService.LikeOffer(offerId, userId);

        return result;
    }
    
    [Authorize]
    [HttpPost("{offerId}/unlike")]
    public ActionResult<bool> UnlikeOffer([FromRoute] string offerId)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = _offerService.UnLikeOffer(offerId, userId);

        return Ok(result);
    }
    
    
}