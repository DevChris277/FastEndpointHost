using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AddressEndpoints.UpdateAddress;

public class UpdateAddressEndpoint : Endpoint<UpdateAddressRequest, AddressResponse, UpdateAddressMapper>
{
    private readonly IAddressRepository _addressRepository;

    public UpdateAddressEndpoint(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    public override void Configure()
    {
        Put("/address/update");
    }
    
    public override async Task HandleAsync(UpdateAddressRequest req, CancellationToken ct)
    {
        if (await _addressRepository.GetAddressByAddressId(req.AddressId) is not Address address)
        {
            ThrowError("Address not found", StatusCodes.Status404NotFound);
            return;
        }

        address.Update(
            req.Province,
            req.City,
            req.Street,
            req.PostalCode);

        await _addressRepository.Update(address);

        AddressResponse response = Map.FromEntity(address);

        await SendAsync(response, cancellation: ct);
        
        
        

    }
}