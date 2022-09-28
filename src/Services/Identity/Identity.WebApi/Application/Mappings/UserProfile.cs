using AutoMapper;
using Identity.WebApi.Application.Models;
using Identity.WebApi.Data.Models;

namespace Identity.WebApi.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<LoginModel, User>();

        CreateMap<RegisterModel, User>();
    }
}