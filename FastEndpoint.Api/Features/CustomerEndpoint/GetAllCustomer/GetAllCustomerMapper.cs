using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.CustomerAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.CustomerEndpoint.GetAllCustomer;

public class GetAllCustomerMapper : ResponseMapper<CustomerResponse,Customer>
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