using Microsoft.Extensions.Caching.Distributed;

namespace Bag.Application.Common.Interfaces.Services;

public interface IDistributedCacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default);
}