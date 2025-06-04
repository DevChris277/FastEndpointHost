using FastEndpoint.Contracts.Address.Responses;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AddressEndpoints.GetAllAddresses;

public class GetAllAddressesEndpoint : EndpointWithoutRequest<List<AddressResponse>>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public GetAllAddressesEndpoint(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("/address/all");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var addresses = await _addressRepository.GetAllAddresses();
        
        var response = _mapper.Map<List<AddressResponse>>(addresses);
        await SendAsync(response, cancellation: ct);
    }
    
}