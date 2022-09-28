using AutoMapper;
using Bag.Application.Dtos.Customers;
using Bag.Domain.Entities;

namespace Bag.Application.Common.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerReadModel>();

        CreateMap<CustomerCreateModel, Customer>()
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CustomerUpdateModel, Customer>()
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}