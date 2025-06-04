using FastEndpoint.Contracts.Account.Responses;

using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.GetAllAccount;

public class GetAllAccountMapper : ResponseMapper<NewAccountResponse, Account>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;

    public GetAllAccountMapper(ICustomerRepository customerRepository, IAddressRepository addressRepository)
    {
        _customerRepository = customerRepository;
        _addressRepository = addressRepository;
    }
    
    public override async Task<NewAccountResponse> FromEntityAsync(Account e, CancellationToken ct)
    {
        var customers = await GetCustomersAsync(e.CustomerIds, ct);
        var addressResult = await GetAccountAddressAsync(e.AddressId.Value);

        return new NewAccountResponse(
            e.Id.Value,
            e.Name,
            e.MobileNumber,
            e.Email,
            customers,
            addressResult
        );
    }

    private async Task<List<CustomerResult>> GetCustomersAsync(IEnumerable<CustomerId> customerIds, CancellationToken ct)
    {
        var customers = new List<CustomerResult>();
    
        foreach (var customerId in customerIds)
        {
            var customerResult = await GetCustomerResultAsync(customerId.Value);
            if (customerResult != null)
            {
                customers.Add(customerResult);
            }
        }
    
        return customers;
    }

    private async Task<CustomerResult?> GetCustomerResultAsync(Guid customerId)
    {
        var customer = await _customerRepository.GetCustomerByCustomerId(customerId);
        if (customer == null)
            return null;

        var customerAddress = await _addressRepository.GetAddressByAddressId(customer.AddressId.Value);
        var addressResult = CreateAddressResult(customerAddress!);

        return new CustomerResult(
            customer.Id.Value,
            customer.FirstName,
            customer.LastName,
            customer.MobileNumber,
            customer.Email,
            addressResult
        );
    }

    private async Task<AddressResult?> GetAccountAddressAsync(Guid addressId)
    {
        var address = await _addressRepository.GetAddressByAddressId(addressId);
        return address != null ? CreateAddressResult(address) : null;
    }

    private static AddressResult CreateAddressResult(Address address)
    {
        return new AddressResult(
            address.Id.Value,
            address.Province,
            address.City,
            address.Street,
            address.PostalCode
        );
    }



}
