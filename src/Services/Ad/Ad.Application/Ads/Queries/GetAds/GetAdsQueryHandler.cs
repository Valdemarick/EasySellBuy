using Ad.Application.Interfaces.Repositories;
using Ad.Application.Dtos;
using AutoMapper;
using MediatR;

namespace Ad.Application.Ads.Queries.GetAds;

public class GetAdsQueryHandler : BaseHandler, IRequestHandler<GetAdsQuery, IEnumerable<AdReadModel>>
{
    private readonly IMapper _mapper;

    public GetAdsQueryHandler(IRepositoryManager repoManager, IMapper mapper) : base(repoManager)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<AdReadModel>> Handle(GetAdsQuery request, CancellationToken cancellationToken)
    {
        var ads = await repoManager.AdRepository.GetAsync(cancellationToken);

        return _mapper.Map<IEnumerable<AdReadModel>>(ads);
    }
}