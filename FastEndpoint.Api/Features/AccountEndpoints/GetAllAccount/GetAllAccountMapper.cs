using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.GetAllAccount;

public class GetAllAccountMapper : ResponseMapper<AccountResponse,Account>
{
    public override AccountResponse FromEntity(Account e) =>
        new(e.Id.Value,
            e.Name,
            e.MobileNumber,
            e.Email,
            e.CustomerIds.Select(customerId => customerId.Value.ToString()).ToList(),
            e.AddressId.Value);
}
