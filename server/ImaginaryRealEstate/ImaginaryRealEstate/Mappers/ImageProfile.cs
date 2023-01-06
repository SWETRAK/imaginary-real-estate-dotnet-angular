using AutoMapper;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Models.Images;

namespace ImaginaryRealEstate.Mappers;

public class ImageProfile: Profile
{
    public ImageProfile()
    {
        CreateMap<Image, ImageOfferResultDto>()
            .ForMember(m => m.Identifier, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(m => m.FrontPhoto, opt => opt.MapFrom(p => p.IsFrontPhoto));
    }
}