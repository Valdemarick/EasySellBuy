using AutoMapper;
using Bag.Application.Dtos.Orders;
using Bag.Domain.Entities;

namespace Bag.Application.Common.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderReadModel>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.UserName))
            .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
            .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.UserName))
            .ForMember(dest => dest.SellerPhoneNumber, opt => opt.MapFrom(src => src.Seller.PhoneNumber));

        CreateMap<OrderCreateModel, Order>()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Seller, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<OrderUpdateModel, Order>()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Seller, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}