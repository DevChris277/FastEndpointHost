namespace FastEndpoint.Contracts.Account.Responses;

public record AccountResponse(
    Guid AccountId,
    string Name,
    string MobileNumber,
    string Email,
    List<string> CustomerIds,
    Guid AddressId);