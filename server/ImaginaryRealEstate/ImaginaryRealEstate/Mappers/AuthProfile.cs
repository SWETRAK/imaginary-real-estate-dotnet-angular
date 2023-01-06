using AutoMapper;
using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Models.Auth;

namespace ImaginaryRealEstate.Mappers;

public class AuthProfile: Profile
{
    public AuthProfile()
    {
        CreateMap<User, UserInfoDto>()
            .ForMember(m => m.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(m => m.FirstName, opt => opt.MapFrom(p => p.FirstName))
            .ForMember(m => m.LastName, opt => opt.MapFrom(p => p.LastName))
            .ForMember(m => m.DateOfBirth, opt => opt.MapFrom(p => p.DateOfBirth.ToString("yyyy-MM-dd")))
            .ForMember(m => m.Role, opt => opt.MapFrom(p => p.Role));

        CreateMap<User, MinimalUserInfoDto>()
            .ForMember(m => m.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(m => m.FirstName, opt => opt.MapFrom(p => p.FirstName))
            .ForMember(m => m.LastName, opt => opt.MapFrom(p => p.LastName));

        CreateMap<RegisterUserWithPasswordDto, User>()
            .ForMember(u => u.Id, opt => opt.Ignore())
            .ForMember(u => u.HashPassword, opt => opt.Ignore())
            .ForMember(u => u.LikedOffers, opt => opt.Ignore())
            .ForMember(u => u.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(u => u.FirstName, opt => opt.MapFrom(p => p.FirstName))
            .ForMember(u => u.LastName, opt => opt.MapFrom(p => p.LastName))
            .ForMember(u => u.DateOfBirth, opt => opt.MapFrom(p => p.DateOfBirth))
            .ForMember(u => u.Role, opt => opt.MapFrom(p => p.Role));
    }
}
