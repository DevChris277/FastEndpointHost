namespace FastEndpoint.Contracts.Account.Requests;

public record UpdateAccountRequest(
    Guid AccountId,
    string Name,
    string MobileNumber,
    string Email,
    Guid AddressId);