using FastEndpoint.Contracts.Account.Requests;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AccountEndpoints.CreateAccount;

public class CreateAccountEndpoint : Endpoint<CreateAccountRequest, AccountResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public CreateAccountEndpoint(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
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
        
        var response = _mapper.Map<AccountResponse>(account);
        await SendAsync(response, cancellation: ct);
    }
}
