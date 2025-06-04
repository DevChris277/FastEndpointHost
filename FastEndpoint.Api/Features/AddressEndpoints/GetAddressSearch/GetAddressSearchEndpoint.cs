using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AddressEndpoints.GetAddressSearch;

public class GetAddressSearchEndpoint : Endpoint<GetAddressesSearchRequest,List<AddressResponse>>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public GetAddressSearchEndpoint(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
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
        
        var response = _mapper.Map<List<AddressResponse>>(addresses);
        await SendAsync(response, cancellation: ct);
    }
}