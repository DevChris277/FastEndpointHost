using FastEndpoint.Api.Interfaces;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;

namespace FastEndpoint.Api.Services;

public class AccountMapperConfig : IAccountMapperConfig
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;

    public AccountMapperConfig(IAddressRepository addressRepository, ICustomerRepository customerRepository)
    {
        _addressRepository = addressRepository;
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerResult>> LoadCustomersForAccount(Account account)
    {
        var customers = new List<CustomerResult>();
        
        foreach (var customerId in account.CustomerIds)
        {
            var customer = await _customerRepository.GetCustomerByCustomerId(customerId.Value);
            if (customer == null) continue;

            var customerAddress = await _addressRepository.GetAddressByAddressId(customer.AddressId.Value);
            var addressResponse = CreateAddressResponse(customerAddress);

            customers.Add(new CustomerResult(customer.Id.Value, customer.FirstName, customer.LastName,
                customer.MobileNumber, customer.Email, addressResponse));
        }
        
        return customers;
    }

    public async Task<AddressResult> LoadAddressForAccount(Account account)
    {
        var address = await _addressRepository.GetAddressByAddressId(account.AddressId.Value);
        return CreateAddressResponse(address);
    }

    private static AddressResult CreateAddressResponse(Domain.AddressAggregate.Address? address) =>
        address != null 
            ? new(address.Id.Value, address.Province, address.City, address.Street, address.PostalCode)
            : new(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
}