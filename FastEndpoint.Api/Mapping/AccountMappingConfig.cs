using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoint.Domain.CustomerAggregate;
using Mapster;

namespace FastEndpoint.Api.Mapping;

public class AccountMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Account, AccountResponse>()
            .Map(dest => dest.AccountId, src => Guid.Parse(src.Id.Value.ToString()));
        
        config.NewConfig<Account, AccountCompleteResponse>()
            .Map(dest => dest.AccountId, src => Guid.Parse(src.Id.Value.ToString()))
            .Ignore(dest => dest.Address);
    }
}
