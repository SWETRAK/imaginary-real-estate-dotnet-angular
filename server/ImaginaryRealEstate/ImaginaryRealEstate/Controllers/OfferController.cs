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
    public async Task<ActionResult<IEnumerable<OfferResultDto>>> GetAllOffers()
    {
        var result = await _offerService.GetOffers();
        return Ok(result);
    }
    
    [HttpGet("{address}")]
    public async Task<ActionResult<IEnumerable<OfferResultDto>>> GetOffersByAddress([FromRoute] string address)
    {
        var result = await _offerService.GetOffersByAddress(address);
        return Ok(result);
    }
    
    [HttpGet("details/{identifier}")]
    public async Task<ActionResult<OfferResultDto>> GetById([FromRoute] string identifier)
    {
        var result = await _offerService.GetOfferById(identifier);
        return Ok(result);
    }

    [Authorize(Roles = $"{Roles.Author},{Roles.Admin}")]
    [HttpPost]
    public async Task<ActionResult<OfferResultDto>> Create([FromBody] NewOfferIncomingDto newOfferDto)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = await _offerService.CreateOffer(newOfferDto, userId);
        return Created($"/offers/{result.Identifier}", result);
    }
    
    [Authorize(Roles = $"{Roles.Author},{Roles.Admin}")]
    [HttpDelete("{guideId}")]
    public async Task<ActionResult> Delete([FromRoute] string guideId)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = await _offerService.DeleteOffer(guideId, userId);

        return NoContent();
    }
    
    [Authorize]
    [HttpPost("{offerId}/like")]
    public async Task<ActionResult<bool>> LikeOffer([FromRoute] string offerId)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = await _offerService.LikeOffer(offerId, userId);
    
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("{offerId}/unlike")]
    public async Task<ActionResult<bool>> UnlikeOffer([FromRoute] string offerId)
    {
        var userId = AuthenticationHelper.GetUserId(this.User);
        var result = await _offerService.UnLikeOffer(offerId, userId);
    
        return Ok(result);
    }
}