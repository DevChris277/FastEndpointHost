namespace FastEndpoint.Contracts.Address.Requests;

public record CreateAddressRequest(
    string Province,
    string City,
    string Street,
    string PostalCode);