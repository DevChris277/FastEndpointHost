using FastEndpoint.Contracts.Customer.Requests;
using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.CustomerAggregate;
using Mapster;

namespace FastEndpoint.Api.Mapping;

public class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CustomerResponse, CustomerResponse>();
        
        // Map from Customer domain entity to CustomerCompleteResponse
        config.NewConfig<Customer, CustomerCompleteResponse>()
            .Map(dest => dest.CustomerId, src => Guid.Parse(src.Id.Value.ToString()) )
            .Map(dest => dest.AccountId, src => src.AccountId.Value)
            .Ignore(dest => dest.Address);
        
        config.NewConfig<Customer, CustomerResponse>()
            .Map(dest => dest.CustomerId, src => Guid.Parse(src.Id.Value.ToString()))
            .Map(dest => dest.AccountId, src => src.AccountId.Value)
            .Map(dest => dest.AddressId, src => src.AddressId.Value);
    }
}