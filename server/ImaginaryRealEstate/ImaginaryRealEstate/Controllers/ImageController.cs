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
    
    [AllowAnonymous]
    [HttpGet("{imageId}")]
    public async Task<IActionResult> DownloadImage([FromRoute] string imageId)
    {
        var resultStream = await _imageService.GetImage(imageId);
        return File(resultStream.Item1.GetBuffer(), resultStream.Item2, "kamil pietrak");
    }
    
    [Authorize(Roles = $"{Roles.Author},{Roles.Admin}")]
    [HttpPost]
    public ActionResult<ImageOfferResultDto> UploadImage([FromForm(Name = "offerGuid")] string offerGuid, [FromForm(Name = "frontPhoto")] bool frontPhoto, IFormFile file)
    {
        var result = _imageService.CreateImage(file, offerGuid, frontPhoto);
        return Ok(result);
    }
}