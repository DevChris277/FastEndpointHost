using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AddressEndpoints.GetAddressByID;

public class GetAddressByIDEndpoint : EndpointWithoutRequest<AddressResponse, GetAddressByIDMapper>
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressByIDEndpoint(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    
    public override void Configure()
    {
        Get("/address/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var addressId = Route<Guid>("id");

        if (await _addressRepository.GetAddressByAddressId(addressId) is not Address address)
        {
            ThrowError("Address not found", StatusCodes.Status404NotFound);
            return;
        }
        
        await SendAsync(Map.FromEntity(address), cancellation: ct);
    }
}