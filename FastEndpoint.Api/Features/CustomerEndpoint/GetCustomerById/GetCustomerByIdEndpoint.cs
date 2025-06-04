using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.CustomerAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.CustomerEndpoint.GetCustomerById;

public class GetCustomerByIdEndpoint : EndpointWithoutRequest<CustomerCompleteResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdEndpoint(ICustomerRepository customerRepository, IMapper mapper, IAddressRepository addressRepository)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public override void Configure()
    {
        Get("/customer/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customers = await _customerRepository.GetAllCustomers();
        CustomerCompleteResponse response = new();


        foreach (var customer in customers)
        {
            var address = await _addressRepository.GetAddressByAddressId(customer.AddressId.Value);
        
            // Use Mapster to map the customer entity to response
            var customerResponse = _mapper.Map<CustomerCompleteResponse>(customer);
        
            // Map the address separately
            customerResponse.Address = _mapper.Map<AddressResponse>(address);
        
            response = customerResponse;
        }

        await SendAsync(response, cancellation: ct);
    }
}