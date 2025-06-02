using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.GetAccountById;

public class GetAccountByIdEndpoint : EndpointWithoutRequest<AccountResponse,GetAccountByIdMapper>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountByIdEndpoint(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
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
        
        await SendAsync(Map.FromEntity(account), cancellation: ct);
    }
}