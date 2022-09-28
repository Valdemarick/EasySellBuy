using Ad.Application.Dtos;
using Ad.Domain.Models.Enums;
using MediatR;

namespace Ad.Application.Ads.Commands.Create;

public class CreateAdCommand : IRequest<AdReadModel>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int UserInfoId { get; set; }
    public int AddressId { get; set; }
    public States State { get; set; }
    public Categories Category { get; set; }
}