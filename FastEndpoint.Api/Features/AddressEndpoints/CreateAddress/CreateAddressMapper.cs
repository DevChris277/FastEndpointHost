using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AddressEndpoints.CreateAddress;

public class CreateAddressMapper : Mapper<CreateAddressRequest,AddressResponse,Address>
{
    public override Address ToEntity(CreateAddressRequest r) 
    {
        Address entity = base.ToEntity(r);
        return entity;
    }

    public override AddressResponse FromEntity(Address e) =>
        new(e.Id.Value,
            e.Province,
            e.City,
            e.Street,
            e.PostalCode);
}