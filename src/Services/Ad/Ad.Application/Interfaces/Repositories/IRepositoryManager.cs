namespace Ad.Application.Interfaces.Repositories;

public interface IRepositoryManager
{
    IAdRepository AdRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}