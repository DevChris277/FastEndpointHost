using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Contracts.Authentication.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.UserAggregate;
using Mapster;

namespace FastEndpoint.Api.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FeUser, AuthenticationResponse>();
    }
}