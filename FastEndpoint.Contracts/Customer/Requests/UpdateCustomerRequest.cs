namespace FastEndpoint.Contracts.Customer.Requests;

public record UpdateCustomerRequest(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string MobileNumber,
    string Email,
    Guid AccountId,
    Guid AddressId);