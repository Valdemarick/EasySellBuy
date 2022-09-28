using Ad.Application.Interfaces.Repositories;
using Exceptions.CustomExceptions;
using MediatR;

namespace Ad.Application.Ads.Commands.Delete;

public class DeleteAdCommandHandler : BaseHandler, IRequestHandler<DeleteAdCommand>
{
    public DeleteAdCommandHandler(IRepositoryManager repoManager) : base(repoManager) { }

    public async Task<Unit> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
    {
        var existed = await repoManager.AdRepository.FindAsync(m => m.Id == request.Id, cancellationToken);
        if (existed is null)
        {
            throw new NotFoundException(nameof(existed), request.Id);
        }

        repoManager.AdRepository.Delete(existed);
        await repoManager.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}