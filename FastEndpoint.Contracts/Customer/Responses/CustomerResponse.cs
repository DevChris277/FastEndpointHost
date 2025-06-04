namespace FastEndpoint.Contracts.Customer.Responses;

public record CustomerResponse(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string MobileNumber,
    string Email,
    Guid AddressId,
    Guid AccountId);