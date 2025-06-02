using FastEndpoint.Contracts.Customer.Responses;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using System.Linq;

namespace FastEndpoint.Api.Features.CustomerEndpoint.GetAllCustomer;

public class GetAllCustomerEndpoint : EndpointWithoutRequest<List<CustomerResponse>,GetAllCustomerMapper>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomerEndpoint(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public override void Configure()
    {
        Get("/customer/all");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customers = await _customerRepository.GetAllCustomers();
        var response = customers.Select(Map.FromEntity).ToList();
        await SendAsync(response, cancellation: ct);
    }
}