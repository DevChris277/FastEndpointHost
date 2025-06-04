using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.AccountAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AccountEndpoints.GetAccountById;

public class GetAccountByIdEndpoint : EndpointWithoutRequest<AccountCompleteResponse>
{
    
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public GetAccountByIdEndpoint(
        IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IAddressRepository addressRepository,
        IMapper mapper)
    {
        _accountRepository = accountRepository;
        _customerRepository = customerRepository;
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("/account/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var accountId = Route<Guid>("id");

        if (await _accountRepository.GetAccountByAccountId(accountId) is not Account account)
        {
            ThrowError("Account not found", StatusCodes.Status404NotFound);
            return;
        }
        
        
        // Get account address first
        var address = await _addressRepository.GetAddressByAddressId(account.AddressId.Value);
    
        // Use Mapster to map the account entity to response
        var response = _mapper.Map<AccountCompleteResponse>(account);
        response.Address = _mapper.Map<AddressResponse>(address);
        response.Customers = new List<CustomerCompleteResponse>();

        // Process customers sequentially
        foreach (var customerId in account.CustomerIds)
        {
            var customer = await _customerRepository.GetCustomerByCustomerId(customerId.Value);
            if (customer != null)
            {
                // Get customer address
                var customerAddress = await _addressRepository.GetAddressByAddressId(customer.AddressId.Value);
            
                var customerResponse = _mapper.Map<CustomerCompleteResponse>(customer);
                customerResponse.Address = _mapper.Map<AddressResponse>(customerAddress);
            
                response.Customers.Add(customerResponse);
            }
        }
        
        await SendAsync(response, cancellation: ct);

    }
}