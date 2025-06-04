using FastEndpoint.Contracts.Address.Responses;
using FastEndpoint.Contracts.Customer.Responses;

namespace FastEndpoint.Contracts.Account.Responses;

public class AccountResponse
{
    public Guid AccountId { get; set; }
    public string Name { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<string>? CustomerIds { get; set; }
    public Guid AddressId { get; set; }
}

public class AccountCompleteResponse
{
    public Guid AccountId { get; set; }
    public string Name { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<CustomerCompleteResponse>? Customers { get; set; }
    public AddressResponse Address { get; set; } = null!;
}
