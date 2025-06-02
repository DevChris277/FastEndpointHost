using FastEndpoint.Contracts.Account.Requests;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.CreateAccount;

public class CreateAccountMapper : Mapper<CreateAccountRequest,AccountResponse,Account>
{
    public override Account ToEntity(CreateAccountRequest r)
    {
        var entity = base.ToEntity(r);
        return entity;
    }

    public override AccountResponse FromEntity(Account e)
    {
        return new AccountResponse(
            e.Id.Value,
            e.Name,
            e.MobileNumber,
            e.Email,
            e.CustomerIds.Select(customerId => customerId.Value.ToString()).ToList(),
            e.AddressId.Value
        );

    }
}