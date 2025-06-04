using FastEndpoint.Contracts.Customer.Requests;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.CustomerEndpoint.UpdateCustomer;

public class UpdateCustomerEndpoint : Endpoint<UpdateCustomerRequest,CustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerEndpoint(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
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
        
        var response =_mapper.Map<CustomerResponse>(customer);
        await SendAsync(response, cancellation: ct);
    }
}