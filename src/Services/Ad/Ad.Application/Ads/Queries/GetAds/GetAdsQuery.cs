using Ad.Application.Dtos;
using MediatR;

namespace Ad.Application.Ads.Queries.GetAds;

public class GetAdsQuery : IRequest<IEnumerable<AdReadModel>> { }