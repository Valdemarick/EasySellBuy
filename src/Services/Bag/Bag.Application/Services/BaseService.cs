using AutoMapper;
using Bag.Application.Common.Interfaces.Repositories;

namespace Bag.Application.Services;

public abstract class BaseService
{
    protected readonly IRepositoryManager repoManager;
    protected readonly IMapper mapper;

    public BaseService(IRepositoryManager repoManager, IMapper mapper)
    {
        this.repoManager = repoManager ?? throw new ArgumentNullException(nameof(repoManager));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
}