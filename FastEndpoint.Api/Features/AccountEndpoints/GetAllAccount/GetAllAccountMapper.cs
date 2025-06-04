using FastEndpoint.Contracts.Account.Responses;

using FastEndpoint.Domain.AccountAggregate;
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
    
    // Synchronous version - required by FastEndpoints
    public override NewAccountResponse FromEntity(Account e)
    {
        return base.FromEntity(e);
    }


    public override async Task<NewAccountResponse> FromEntityAsync(Account e, CancellationToken ct)
    {
        var customers = new List<CustomerResult>();
        
        // Fetch customers by their IDs
        foreach (var customerId in e.CustomerIds)
        {
            var customer = await _customerRepository.GetCustomerByCustomerId(customerId.Value);
            if (customer != null)
            {
                var customerAddress = await _addressRepository.GetAddressByAddressId(customer.AddressId.Value);
                customers.Add(new CustomerResult(
                    customer.Id.Value,
                    customer.FirstName,
                    customer.LastName,
                    customer.MobileNumber,
                    customer.Email,
                    new AddressResult(
                        customerAddress.Id.Value,
                        customerAddress.Province,
                        customerAddress.City,
                        customerAddress.Street,
                        customerAddress.PostalCode
                    )
                ));
            }
        }

        // Fetch account address
        var address = await _addressRepository.GetAddressByAddressId(e.AddressId.Value);
        var addressResult = address != null ? new AddressResult(
            address.Id.Value,
            address.Province,
            address.City,
            address.Street,
            address.PostalCode
        ) : null;

        return new NewAccountResponse(
            e.Id.Value,
            e.Name,
            e.MobileNumber,
            e.Email,
            customers,
            addressResult
        );
    }


}
