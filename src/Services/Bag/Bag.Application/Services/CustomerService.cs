using AutoMapper;
using Bag.Application.Common.Interfaces.Repositories;
using Bag.Application.Common.Interfaces.Services;
using Bag.Application.Dtos.Customers;
using Bag.Domain.Entities;
using Exceptions.CustomExceptions;

namespace Bag.Application.Services;

public class CustomerService : BaseService, ICustomerService
{
    public CustomerService(IRepositoryManager repoManager, IMapper mapper) : base(repoManager, mapper) { }

    public async Task<IEnumerable<CustomerReadModel>> GetAsync(CancellationToken cancellationToken = default)
    {
        var customers = await repoManager.CustomerRepository.GetAsync(cancellationToken);

        return mapper.Map<IEnumerable<CustomerReadModel>>(customers);
    }

    public async Task<CustomerReadModel?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await repoManager.CustomerRepository.GetAsync(id, cancellationToken);

        return mapper.Map<CustomerReadModel>(customer);
    }

    public async Task<CustomerReadModel> CreateAsync(CustomerCreateModel model, CancellationToken cancellationToken = default)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var customer = mapper.Map<Customer>(model);

        var createdCustomer = await repoManager.CustomerRepository.CreateAsync(customer, cancellationToken);
        await repoManager.SaveChangesAsync(cancellationToken);

        return mapper.Map<CustomerReadModel>(createdCustomer);
    }

    public async Task UpdateAsync(int id, CustomerUpdateModel model, CancellationToken cancellationToken = default)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var existingCustomer = await repoManager.CustomerRepository.FindAsync(m => m.Id == id);
        if (existingCustomer is null)
        {
            throw new NotFoundException($"The customer with id: {id} doesn't exist in the database");
        }

        mapper.Map(model, existingCustomer);

        await repoManager.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existingCustomer = await repoManager.CustomerRepository.FindAsync(m => m.Id == id);
        if (existingCustomer is null)
        {
            throw new NotFoundException($"The customer with id: {id} doesn't exist in the database");
        }

        repoManager.CustomerRepository.Delete(existingCustomer);
        await repoManager.SaveChangesAsync(cancellationToken);
    }
}