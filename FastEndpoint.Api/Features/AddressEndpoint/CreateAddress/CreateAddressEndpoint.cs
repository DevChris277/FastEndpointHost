using FastEndpoint.Api.Features.AddressEndpoint.Results;
using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;


namespace FastEndpoint.Api.Features.AddressEndpoint.CreateAddress;

public class CreateAddressEndpoint
    : Endpoint<CreateAddressRequest, AddressResponse>
{
    
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;
    
    public CreateAddressEndpoint(IMapper mapper, IAddressRepository addressRepository)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public override void Configure()
    {
        Post("/address/create");
    }

    public override async Task HandleAsync(CreateAddressRequest req, CancellationToken ct)
    {
        var command = _mapper.Map<CreateAddressCommand>(req);
        
        if (await _addressRepository.GetAddressByStreetNameAndPostalCode(command.Street, command.PostalCode) is Address addressExists)
            ThrowError("Address already exists.","Address.Duplicate");
        
        var address = Address.Create(
            command.Province,
            command.City,
            command.Street,
            command.PostalCode);
        
        await _addressRepository.Add(address);
        
        var result = new AddressResult(
            address,
            address.Id.Value);

        await SendAsync(_mapper.Map<AddressResponse>(result), cancellation: ct);

    }
}