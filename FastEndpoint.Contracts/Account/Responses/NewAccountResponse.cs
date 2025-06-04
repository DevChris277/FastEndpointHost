namespace FastEndpoint.Contracts.Account.Responses;

public record NewAccountResponse(
    Guid AccountId,
    string Name,
    string MobileNumber,
    string Email,
    List<CustomerResult> Customers,
    AddressResult? Address);

public record CustomerResult(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string MobileNumber,
    string Email,
    AddressResult Address);

public record AddressResult(
    Guid AddressId,
    string Province,
    string City,
    string Street,
    string PostalCode);
