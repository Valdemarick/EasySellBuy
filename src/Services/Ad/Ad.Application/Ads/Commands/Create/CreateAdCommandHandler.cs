using Ad.Application.Interfaces.Repositories;
using Ad.Application.Dtos;
using Ad.Domain.Models;
using AutoMapper;
using MediatR;

namespace Ad.Application.Ads.Commands.Create;

public class CreateAdCommandHandler : BaseHandler, IRequestHandler<CreateAdCommand, AdReadModel>
{
    private readonly IMapper _mapper;

    public CreateAdCommandHandler(IRepositoryManager repoManager, IMapper mapper) : base(repoManager)
    {
        _mapper = mapper;
    }

    public async Task<AdReadModel> Handle(CreateAdCommand request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var ad = _mapper.Map<AdModel>(request);

        var createdAd = await repoManager.AdRepository.CreateAsync(ad, cancellationToken);
        await repoManager.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AdReadModel>(createdAd);
    }
}