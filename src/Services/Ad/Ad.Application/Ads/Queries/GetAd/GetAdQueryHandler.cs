using Ad.Application.Interfaces.Repositories;
using Ad.Application.Dtos;
using AutoMapper;
using MediatR;

namespace Ad.Application.Ads.Queries.GetAd;

public class GetAdQueryHandler : BaseHandler, IRequestHandler<GetAdQuery, AdReadModel>
{
    private readonly IMapper _mapper;

    public GetAdQueryHandler(IRepositoryManager repoManager, IMapper mapper) : base(repoManager)
    {
        _mapper = mapper;
    }

    public async Task<AdReadModel> Handle(GetAdQuery request, CancellationToken cancellationToken)
    {
        var ad = await repoManager.AdRepository.GetAsync(request.Id, cancellationToken);

        return _mapper.Map<AdReadModel>(ad);
    }
}