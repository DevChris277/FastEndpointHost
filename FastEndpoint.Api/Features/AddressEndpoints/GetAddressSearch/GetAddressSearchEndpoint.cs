using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.AddressEndpoints.GetAddressSearch;

public class GetAddressSearchEndpoint : Endpoint<GetAddressesSearchRequest,List<AddressResponse>,GetAddressSearchMapper>
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressSearchEndpoint(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    
    public override void Configure()
    {
        Get("/address/search");
    }

    public override async Task HandleAsync(GetAddressesSearchRequest req, CancellationToken ct)
    {
        if (await _addressRepository.GetAddressesSearch(req.SearchString) is not List<Address> addresses)
        {
            ThrowError("Address not found", StatusCodes.Status404NotFound);
            return;
        }
        
        var response = addresses.Select(Map.FromEntity).ToList();
        await SendAsync(response, cancellation: ct);
    }
}