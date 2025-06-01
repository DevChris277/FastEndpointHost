using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Contracts.Address.Requests;
using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoints;


namespace FastEndpoint.Api.Features.AddressEndpoints.CreateAddress;

public class CreateAddressEndpoint : Endpoint<CreateAddressRequest, AddressResponse, CreateAddressMapper>
{
    
    private readonly IAddressRepository _addressRepository;
    
    public CreateAddressEndpoint(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
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
        
        AddressResponse response = Map.FromEntity(address);

        await SendAsync(response, cancellation: ct);

    }
}