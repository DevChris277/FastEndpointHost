namespace FastEndpoint.Contracts.Address.Responses;

public class AddressResponse
{
    public Guid AddressId { get; set; }
    public string Province { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}
