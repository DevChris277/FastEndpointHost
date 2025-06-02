using FastEndpoint.Contracts.Account.Requests;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AccountEndpoints.UpdateAccount;

public class UpdateAccountMapper : Mapper<UpdateAccountRequest,AccountResponse,Account>
{
    public override AccountResponse FromEntity(Account e)
    {
        var response = base.FromEntity(e);
        return response; 
    }
    
    public override Account ToEntity(UpdateAccountRequest r)
    {        
        return base.ToEntity(r);
    }
}