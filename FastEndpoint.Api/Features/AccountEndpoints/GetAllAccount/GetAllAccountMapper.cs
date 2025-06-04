using FastEndpoint.Api.Interfaces;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.AccountAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.GetAllAccount;

public class GetAllAccountMapper : ResponseMapper<NewAccountResponse, Account>
{
    private readonly IAccountMapperConfig _accountMapperConfig;

    public GetAllAccountMapper(IAccountMapperConfig accountMapperConfig)
    {
        _accountMapperConfig = accountMapperConfig;
    }


    public NewAccountResponse FromEntity(Account e)
    {
        var customers = _accountMapperConfig.LoadCustomersForAccount(e).GetAwaiter().GetResult();
        var address = _accountMapperConfig.LoadAddressForAccount(e).GetAwaiter().GetResult();
        
        return new(e.Id.Value, e.Name, e.MobileNumber, e.Email, customers, address);
    }

}
