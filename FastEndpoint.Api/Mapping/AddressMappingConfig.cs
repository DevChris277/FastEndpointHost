using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using Mapster;

namespace FastEndpoint.Api.Mapping;

public class AddressMappingConfig: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Address, AddressResponse>()
            .Map(dest => dest.AddressId, src => Guid.Parse(src.Id.Value.ToString()));
    }
}