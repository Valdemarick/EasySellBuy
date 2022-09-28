using AutoMapper;
using Bag.Application.Dtos.Sellers;
using Bag.Domain.Entities;

namespace Bag.Application.Common.Mappings;

public class SellerProfile : Profile
{
    public SellerProfile()
    {
        CreateMap<Seller, SellerReadModel>();

        CreateMap<SellerCreateModel, Seller>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore());

        CreateMap<SellerUpdateModel, Seller>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore());
    }
}