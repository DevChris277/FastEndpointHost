namespace FastEndpoint.Contracts.Address.Requests;

public record UpdateAddressRequest(
    Guid AddressId,
    string Province,
    string City,
    string Street,
    string PostalCode);