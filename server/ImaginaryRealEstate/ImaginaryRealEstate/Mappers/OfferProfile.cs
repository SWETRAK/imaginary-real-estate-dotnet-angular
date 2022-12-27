using AutoMapper;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Models.Offers;

namespace ImaginaryRealEstate.Mappers;

public class OfferProfile: Profile
{
    public OfferProfile()
    {
        CreateMap<Offer, OfferResultDto>()
            .ForMember(m => m.Identifier, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(m => m.Title, opt => opt.MapFrom(p => p.Title))
            .ForMember(m => m.Description, opt => opt.MapFrom(p => p.Description))
            .ForMember(m => m.Address, opt => opt.MapFrom(p => p.Address))
            .ForMember(m => m.Author, opt => opt.MapFrom(p => p.Author))
            .ForMember(m => m.Price, opt => opt.MapFrom(p => p.Price))
            .ForMember(m => m.Bedrooms, opt => opt.MapFrom(p => p.Bedrooms))
            .ForMember(m => m.Bathrooms, opt => opt.MapFrom(p => p.Bathrooms))
            .ForMember(m => m.Area, opt => opt.MapFrom(p => p.Area))
            .ForMember(m => m.Images, opt => opt.MapFrom(p => p.Images));

        CreateMap<NewOfferIncomingDto, Offer>()
            .ForMember(m => m.Id, opt => opt.Ignore())
            .ForMember(m => m.Images, opt => opt.Ignore())
            .ForMember(m => m.Author, opt => opt.Ignore())
            .ForMember(m => m.AuthorId, opt => opt.Ignore())
            .ForMember(m => m.Title, opt => opt.MapFrom(p => p.Title))
            .ForMember(m => m.Description, opt => opt.MapFrom(p => p.Description))
            .ForMember(m => m.Address, opt => opt.MapFrom(p => p.Address))
            .ForMember(m => m.Area, opt => opt.MapFrom(p => p.Area))
            .ForMember(m => m.Bathrooms, opt => opt.MapFrom(p => p.Bathrooms))
            .ForMember(m => m.Bedrooms, opt => opt.MapFrom(p => p.Bedrooms))
            .ForMember(m => m.Price, opt => opt.MapFrom(p => p.Price));
    }
}