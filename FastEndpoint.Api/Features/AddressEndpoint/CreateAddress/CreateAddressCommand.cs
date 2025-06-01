namespace FastEndpoint.Api.Features.AddressEndpoint.CreateAddress;


public record CreateAddressCommand(
    string Province,
    string City,
    string Street,
    string PostalCode);
