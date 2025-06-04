using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Contracts.Customer.Responses;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AccountEndpoints.GetAllAccount;

public class GetAllAccountEndpoint : EndpointWithoutRequest<List<AccountCompleteResponse>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public GetAllAccountEndpoint(
        IAccountRepository accountRepository,
        IMapper mapper,
        IAddressRepository addressRepository,
        ICustomerRepository customerRepository)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _addressRepository = addressRepository;
        _customerRepository = customerRepository;
    }
    
    public override void Configure()
    {
        Get("/account/all");
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var accounts = await _accountRepository.GetAllAccounts();
        List<AccountCompleteResponse> response = new();

        foreach (var account in accounts)
        {
            // Get account address first
            var address = await _addressRepository.GetAddressByAddressId(account.AddressId.Value);
        
            // Use Mapster to map the account entity to response
            var accountResponse = _mapper.Map<AccountCompleteResponse>(account);
            accountResponse.Address = _mapper.Map<AddressResponse>(address);
            accountResponse.Customers = new List<CustomerCompleteResponse>();

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
                
                    accountResponse.Customers.Add(customerResponse);
                }
            }
        
            response.Add(accountResponse);
        }
    
        await SendAsync(response, cancellation: ct);
      
    }
}