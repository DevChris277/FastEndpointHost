using FastEndpoint.Api.Features.AddressEndpoint.CreateAddress;
using FastEndpoint.Api.Features.AddressEndpoint.Results;
using FastEndpoint.Contracts.Account.Requests;
using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using Mapster;

namespace FastEndpoint.Api.Features.AddressEndpoint;

public class AddressMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddressResult, AddressResponse>()
            .Map(dest => dest, src => src.Address);
    }
}