using FastEndpoint.Contracts.Customer.Requests;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using FastEndpoint.Domain.CustomerAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.CustomerEndpoint.CreateCustomer;

public class CreateCustomerEndpoint : Endpoint<CreateCustomerRequest,CustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerEndpoint(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    
    public override void Configure()
    {
        Post("/customer/create");
        PostProcessor<CreateCustomerEvent<CreateCustomerRequest,CustomerResponse>>();
    }

    public override async Task HandleAsync(CreateCustomerRequest req, CancellationToken ct)
    {
        if(await _customerRepository.GetCustomerByEmail(req.Email) is Customer)
            ThrowError("Customer already exists", StatusCodes.Status400BadRequest);
        
        var customer = Customer.Create(
            req.FirstName,
            req.LastName,
            req.MobileNumber,
            req.Email,
            AccountId.Create(req.AccountId),
            AddressId.Create(req.AddressId));
        
        await _customerRepository.Add(customer);
        
        var response = _mapper.Map<CustomerResponse>(customer);
        await SendAsync(response, cancellation: ct);
    }
}