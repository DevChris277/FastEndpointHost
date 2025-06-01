using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AddressEndpoints.GetAddressByID;

public class GetAddressByIDMapper : ResponseMapper<AddressResponse,Address>
{
    public override AddressResponse FromEntity(Address e) =>
        new(e.Id.Value,
            e.Province,
            e.City,
            e.Street,
            e.PostalCode);
}