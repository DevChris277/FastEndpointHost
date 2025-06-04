using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AddressEndpoints.GetAddressByID;

public class GetAddressByIDEndpoint : EndpointWithoutRequest<AddressResponse>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public GetAddressByIDEndpoint(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
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
        
        var response = _mapper.Map<AddressResponse>(address);
        await SendAsync(response, cancellation: ct);
    }
}