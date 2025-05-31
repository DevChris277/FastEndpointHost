using FastEndpoint.Domain.CustomerAggregate;

namespace FastEndpoint.Application.Interfaces.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerByEmail(string email);
    Task<Customer?> GetCustomerByCustomerId(Guid id);
    Task<List<Customer>> GetAllCustomers();
    Task Add(Customer customer);
    Task Update(Customer customer);
}