using AutoMapper;
using Bag.Application.Common.Interfaces.Repositories;
using Bag.Application.Common.Interfaces.Services;
using Bag.Application.Dtos.Sellers;
using Bag.Domain.Entities;
using Exceptions.CustomExceptions;

namespace Bag.Application.Services;

public class SellerService : BaseService, ISellerService
{
    public SellerService(IRepositoryManager repoManager, IMapper mapper) : base(repoManager, mapper) { }

    public async Task<IEnumerable<SellerReadModel>> GetAsync(CancellationToken cancellationToken = default)
    {
        var sellers = await repoManager.SellerRepository.GetAsync(cancellationToken);

        return mapper.Map<IEnumerable<SellerReadModel>>(sellers);
    }

    public async Task<SellerReadModel?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var seller = await repoManager.SellerRepository.GetAsync(id, cancellationToken);

        return mapper.Map<SellerReadModel>(seller);
    }

    public async Task<SellerReadModel> CreateAsync(SellerCreateModel model, CancellationToken cancellationToken = default)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var seller = mapper.Map<Seller>(model);

        var createdSeller = await repoManager.SellerRepository.CreateAsync(seller, cancellationToken);
        await repoManager.SaveChangesAsync(cancellationToken);

        return mapper.Map<SellerReadModel>(createdSeller);
    }

    public async Task UpdateAsync(int id, SellerUpdateModel model, CancellationToken cancellationToken = default)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var existingSeller =  await repoManager.SellerRepository.FindAsync(
            m => m.Id == id, cancellationToken);
        if (existingSeller is null)
        {
            throw new NotFoundException($"The seller with id: {id} doesn't exist in the database");
        }

        mapper.Map(model, existingSeller);

        await repoManager.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existingSeller =  await repoManager.SellerRepository.FindAsync(
            m => m.Id == id, cancellationToken);
        if (existingSeller is null)
        {
            throw new NotFoundException($"The seller with id: {id} doesn't exist in the database");
        }

        repoManager.SellerRepository.Delete(existingSeller);
        await repoManager.SaveChangesAsync(cancellationToken);
    }
}