using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using Imapper = MapsterMapper.IMapper;


namespace FastEndpoint.Api.Features.AddressEndpoints.CreateAddress;

public class CreateAddressEndpoint : Endpoint<CreateAddressRequest, AddressResponse>
{
    
    private readonly IAddressRepository _addressRepository;
    private readonly Imapper _mapper;
    
    public CreateAddressEndpoint(IAddressRepository addressRepository, Imapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Post("/address/create"); 
    }

    public override async Task HandleAsync(CreateAddressRequest req, CancellationToken ct)
    {
        if (await _addressRepository.GetAddressByStreetNameAndPostalCode(req.Street, req.PostalCode) is Address addressExists)
            ThrowError("Address already exists.","Address.Duplicate");
        
        var address = Address.Create(
            req.Province,
            req.City,
            req.Street,
            req.PostalCode);
        
        await _addressRepository.Add(address);
        
        AddressResponse response = _mapper.Map<AddressResponse>(address);

        await SendAsync(response, cancellation: ct);

    }
}