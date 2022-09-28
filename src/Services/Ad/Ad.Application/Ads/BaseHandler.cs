using Ad.Application.Interfaces.Repositories;

namespace Ad.Application.Ads;

public abstract class BaseHandler
{
    protected readonly IRepositoryManager repoManager;

    public BaseHandler(IRepositoryManager repoManager)
    {
        this.repoManager = repoManager ?? throw new ArgumentNullException(nameof(repoManager));
    }
}