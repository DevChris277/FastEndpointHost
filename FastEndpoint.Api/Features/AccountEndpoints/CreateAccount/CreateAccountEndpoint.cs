using FastEndpoint.Contracts.Account.Requests;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.CreateAccount;

public class CreateAccountEndpoint : Endpoint<CreateAccountRequest, AccountResponse, CreateAccountMapper>
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountEndpoint(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public override void Configure()
    {
        Post("/account/create");
    }

    public override async Task HandleAsync(CreateAccountRequest req, CancellationToken ct)
    {
        if (await _accountRepository.GetAccountByEmail(req.Email) is Account)
            ThrowError("Account already exists", StatusCodes.Status400BadRequest);
        
        var account = Account.Create(
            req.Name,
            req.MobileNumber,
            req.Email,
            AddressId.Create(req.AddressId));
        
        await _accountRepository.Add(account);
        
        var response = Map.FromEntity(account);
        await SendAsync(response, cancellation: ct);
    }
}
