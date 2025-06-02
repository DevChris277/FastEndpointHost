using FastEndpoint.Contracts.Account.Requests;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.UpdateAccount;

public class UpdateAccountMapper : Mapper<UpdateAccountRequest,AccountResponse,Account>
{
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
    
    public override Account ToEntity(UpdateAccountRequest r)
    {        
        return base.ToEntity(r);
    }
}