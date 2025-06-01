using FastEndpoint.Domain.CustomerAggregate;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FastEndpoint.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly FepDataContext _dbContext;

    public CustomerRepository(FepDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Customer customer)
    {
        await _dbContext.AddAsync(customer);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _dbContext.Customer
            .ToListAsync();
    }

    public async Task<Customer?> GetCustomerByCustomerId(Guid id)
    {
        var customerId = CustomerId.Create(id);

        return await _dbContext.Customer
            .FirstOrDefaultAsync(a => a.Id == customerId);
    }

    public async Task<Customer?> GetCustomerByEmail(string email)
    {
        return await _dbContext.Customer
            .FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task Update(Customer customer)
    {
        _dbContext.Update(customer);

        await _dbContext.SaveChangesAsync();
    }
}