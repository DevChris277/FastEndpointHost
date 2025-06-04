using FastEndpoint.Contracts.Address.Responses;

namespace FastEndpoint.Contracts.Customer.Responses;

public class CustomerResponse
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid AccountId { get; set; }
    public Guid AddressId { get; set; }
}

public class CustomerCompleteResponse
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid AccountId { get; set; }
    public AddressResponse Address { get; set; } = null!;
}

