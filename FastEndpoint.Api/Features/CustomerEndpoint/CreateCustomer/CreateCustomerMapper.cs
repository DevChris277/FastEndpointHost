using FastEndpoint.Contracts.Customer.Requests;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.CustomerAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.CustomerEndpoint.CreateCustomer;

public class CreateCustomerMapper : Mapper<CreateCustomerRequest,CustomerResponse,Customer>
{
    public override CustomerResponse FromEntity(Customer e) => 
        new CustomerResponse
        {
            CustomerId = e.Id.Value,
            FirstName = e.FirstName,
            LastName = e.LastName,
            MobileNumber = e.MobileNumber,
            Email = e.Email,
            AccountId = e.AccountId.Value,
            AddressId = e.AddressId.Value
        };

}