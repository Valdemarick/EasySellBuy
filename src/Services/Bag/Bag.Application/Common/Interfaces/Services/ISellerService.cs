using Bag.Application.Dtos.Sellers;

namespace Bag.Application.Common.Interfaces.Services;

public interface ISellerService
{
    Task<IEnumerable<SellerReadModel>> GetAsync(CancellationToken cancellationToken = default);
    Task<SellerReadModel?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<SellerReadModel> CreateAsync(SellerCreateModel model, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, SellerUpdateModel model, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}