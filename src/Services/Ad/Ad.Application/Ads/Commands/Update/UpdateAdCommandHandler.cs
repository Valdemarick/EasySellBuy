using Ad.Application.Interfaces.Repositories;
using Ad.Domain.Models;
using AutoMapper;
using Exceptions.CustomExceptions;
using MediatR;

namespace Ad.Application.Ads.Commands.Update;

public class UpdateAdCommandHandler : BaseHandler, IRequestHandler<UpdateAdCommand>
{
    private readonly IMapper _mapper;

    public UpdateAdCommandHandler(IRepositoryManager repoManager, IMapper mapper) : base(repoManager)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var existingAd = await repoManager.AdRepository.FindAsync(m => m.Id == request.Id, cancellationToken);
        if (existingAd is null)
        {
            throw new NotFoundException(nameof(existingAd), request.Id);
        }

        _mapper.Map(request, existingAd);

        await repoManager.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}