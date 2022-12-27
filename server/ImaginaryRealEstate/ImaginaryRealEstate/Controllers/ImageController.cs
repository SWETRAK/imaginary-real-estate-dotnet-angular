using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Models.Images;
using ImaginaryRealEstate.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImaginaryRealEstate.Controllers;

[ApiController]
[Route("images")]
public class ImageController: Controller
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }
    
    [Authorize(Roles = $"{Roles.Author},{Roles.Admin}")]
    [HttpPost]
    public ActionResult<ImageOfferResultDto> TestImage([FromForm(Name = "offerGuid")] string offerGuid, [FromForm(Name = "frontPhoto")] bool frontPhoto, IFormFile file)
    {
        Console.WriteLine(offerGuid);
        var result = _imageService.CreateImage(file, offerGuid, frontPhoto);
        return Ok(result);
    }
    
    [HttpGet("{imageId}")]
    public async Task<IActionResult> Download([FromRoute] string imageId)
    {
        var resultStream = await _imageService.GetImage(imageId);
        return File(resultStream.Item1.GetBuffer(), resultStream.Item2, "kamil pietrak");
    }
}