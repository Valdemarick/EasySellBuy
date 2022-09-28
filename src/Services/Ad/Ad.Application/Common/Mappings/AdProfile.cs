using Ad.Application.Ads.Commands.Create;
using Ad.Application.Ads.Commands.Update;
using Ad.Application.Dtos;
using Ad.Domain.Models;
using AutoMapper;

namespace Ad.Application.Common.Mappings;

public class AdProfile : Profile
{
    public AdProfile()
    {
        CreateMap<AdModel, AdReadModel>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserInfo.UserName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserInfo.PhoneNumber))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City.Name))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country.Name));

        CreateMap<CreateAdCommand, AdModel>();

        CreateMap<UpdateAdCommand, AdModel>()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore());
    }
}