namespace FastEndpoint.Contracts.Customer.Requests;

public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string MobileNumber,
    string Email,
    Guid AccountId,
    Guid AddressId);