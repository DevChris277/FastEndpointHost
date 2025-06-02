using FastEndpoint.Contracts.Account.Requests;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.UpdateAccount;

public class UpdateAccountEndpoint : Endpoint<UpdateAccountRequest,AccountResponse,UpdateAccountMapper>
{
    private readonly IAccountRepository _accountRepository;
    public UpdateAccountEndpoint(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public override void Configure()
    {
        Put("/account/update");
    }
    
    public override async Task HandleAsync(UpdateAccountRequest req, CancellationToken ct)
    {
        if (await _accountRepository.GetAccountByAccountId(req.AccountId) is not { } account)
        {
            ThrowError("Account not found", StatusCodes.Status404NotFound);
            return;
        }
        
        account.Update(
            req.Name,
            req.MobileNumber,
            req.Email,
            AddressId.Create(req.AddressId));
        
        await _accountRepository.Update(account);
        
        var response = Map.FromEntity(account);
        await SendAsync(response, cancellation: ct);
    }
}