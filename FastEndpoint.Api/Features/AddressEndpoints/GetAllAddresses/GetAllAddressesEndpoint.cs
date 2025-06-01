using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AddressEndpoints.GetAllAddresses;

public class GetAllAddressesEndpoint : EndpointWithoutRequest<List<AddressResponse>, GetAddressSearchMapper>
{
    private readonly IAddressRepository _addressRepository;

    public GetAllAddressesEndpoint(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public override void Configure()
    {
        Get("/address/all");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var addresses = await _addressRepository.GetAllAddresses();
        
        var response = addresses.Select(Map.FromEntity).ToList();
        await SendAsync(response, cancellation: ct);
    }
    
}