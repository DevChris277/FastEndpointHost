using FastEndpoint.Api.Features.AuthenticationEndpoints.LoginEndpoint;
using FastEndpoint.Api.Features.AuthenticationEndpoints.RegisterEndpoint;
using FastEndpoint.Api.Features.AuthenticationEndpoints.Results;
using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoints.Contracts.Authentication.Responses;
using Mapster;

namespace FastEndpoint.Api.Features.AuthenticationEndpoints;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}