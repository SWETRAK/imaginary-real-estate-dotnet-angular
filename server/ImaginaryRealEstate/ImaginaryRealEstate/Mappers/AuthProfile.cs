using AutoMapper;
using ImaginaryRealEstate.Entities;
using ImaginaryRealEstate.Models.Auth;

namespace ImaginaryRealEstate.Mappers;

public class AuthProfile: Profile
{
    public AuthProfile()
    {
        CreateMap<User, UserInfoDto>()
            .ForMember(m => m.Email, opt => opt.MapFrom(p => p.Email))
            .ForMember(m => m.Token, opt => opt.Ignore())
            .ForMember(m => m.FirstName, opt => opt.MapFrom(p => p.FirstName))
            .ForMember(m => m.LastName, opt => opt.MapFrom(p => p.LastName))
            .ForMember(m => m.DateOfBirth, opt => opt.MapFrom(p => p.DateOfBirth.ToString("yyyy-MM-dd")))
            .ForMember(m => m.Role, opt => opt.MapFrom(p => p.Role));
    }
}