using FastEndpoint.Contracts.Customer.Requests;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using FastEndpoint.Domain.CustomerAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.CustomerEndpoint.UpdateCustomer;

public class UpdateCustomerEndpoint : Endpoint<UpdateCustomerRequest,CustomerResponse,UpdateCustomerMapper>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerEndpoint(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override void Configure()
    {
        Put("/customer/update");
    }

    public override async Task HandleAsync(UpdateCustomerRequest req, CancellationToken ct)
    {
        if (await _customerRepository.GetCustomerByCustomerId(req.CustomerId) is not {} customer)
        {
            ThrowError("Customer not found", StatusCodes.Status404NotFound);
            return;
        }

        customer.Update(
            req.FirstName,
            req.LastName,
            req.MobileNumber,
            req.Email,
            AccountId.Create(req.AccountId),
            AddressId.Create(req.AddressId));
        
        await _customerRepository.Update(customer);
        
        var response = Map.FromEntity(customer);
        await SendAsync(response, cancellation: ct);
    }
}