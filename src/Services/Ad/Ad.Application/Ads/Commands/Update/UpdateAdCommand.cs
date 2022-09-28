using Ad.Domain.Models.Enums;
using MediatR;

namespace Ad.Application.Ads.Commands.Update;

public class UpdateAdCommand : IRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int UserInfoId { get; set; }
    public int AddressId { get; set; }
    public States State { get; set; }
    public Categories Category { get; set; }
}