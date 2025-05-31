namespace FastEndpoint.Contracts.Address.Responses;

public record AddressResponse(
    Guid AddressId,
    string Province,
    string City,
    string Street,
    string PostalCode);