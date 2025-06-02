using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.CustomerAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.CustomerEndpoint.GetCustomerById;

public class GetCustomerByIdEndpoint : EndpointWithoutRequest<CustomerResponse,GetCustomerByIdMapper>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdEndpoint(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override void Configure()
    {
        Get("/customer/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customerId = Route<Guid>("id");

        if (await _customerRepository.GetCustomerByCustomerId(customerId) is not Customer customer)
        {
            ThrowError("Customer not found", StatusCodes.Status404NotFound);
            return;
        }
        
        await SendAsync(Map.FromEntity(customer), cancellation: ct);
    }
}