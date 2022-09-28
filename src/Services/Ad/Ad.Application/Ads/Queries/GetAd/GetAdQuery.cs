using Ad.Application.Dtos;
using MediatR;

namespace Ad.Application.Ads.Queries.GetAd;

public class GetAdQuery : IRequest<AdReadModel>
{
    public int Id { get; set; }
}