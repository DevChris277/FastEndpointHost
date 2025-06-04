using FastEndpoint.Contracts.Account.Responses;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.GetAllAccount;

public class GetAllAccountEndpoint : EndpointWithoutRequest<List<NewAccountResponse>,GetAllAccountMapper>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;

    public GetAllAccountEndpoint(IAccountRepository accountRepository, IAddressRepository addressRepository, ICustomerRepository customerRepository)
    {
        _accountRepository = accountRepository;
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
        
        var response = new List<NewAccountResponse>();
        foreach (var account in accounts)
        {
            var mappedAccount = await Map.FromEntityAsync(account, ct);
            response.Add(mappedAccount);
        }

        await SendAsync(response.ToList(), cancellation: ct);
    }
}