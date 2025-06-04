using FastEndpoint.Contracts.Account.Responses;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.GetAllAccount;

public class GetAllAccountEndpoint : EndpointWithoutRequest<List<NewAccountResponse>,GetAllAccountMapper>
{
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountEndpoint(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public override void Configure()
    {
        Get("/account/all");
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var accounts = await _accountRepository.GetAllAccounts();
        
        // Use the FastEndpoint mapper pattern
        var response = accounts.Select(Map.FromEntity).ToList();

        await SendAsync(response, cancellation: ct);
    }
}