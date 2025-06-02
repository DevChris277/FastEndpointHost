using FastEndpoint.Contracts.Customer.Requests;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.CustomerAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.CustomerEndpoint.CreateCustomer;

public class CreateCustomerMapper : Mapper<CreateCustomerRequest,CustomerResponse,Customer>
{
    public override CustomerResponse FromEntity(Customer e) => 
        new(e.Id.Value,
            e.FirstName,
            e.LastName,
            e.MobileNumber,
            e.Email,
            e.AccountId.Value,
            e.AddressId.Value);
}