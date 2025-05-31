namespace FastEndpoint.Contracts.Account.Requests;

public record CreateAccountRequest(
    string Name,
    string MobileNumber,
    string Email,
    Guid AddressId);