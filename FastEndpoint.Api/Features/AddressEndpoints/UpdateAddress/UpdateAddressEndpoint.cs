using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AddressEndpoints.UpdateAddress;

public class UpdateAddressEndpoint : Endpoint<UpdateAddressRequest, AddressResponse>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public UpdateAddressEndpoint(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
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

        var response = _mapper.Map<AddressResponse>(address);

        await SendAsync(response, cancellation: ct);
        
        
        

    }
}