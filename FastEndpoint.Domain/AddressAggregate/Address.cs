using FastEndpoint.Domain.AddressAggregate.ValueObject;
using FastEndpoint.Domain.Common.Models;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;

namespace FastEndpoint.Domain.AddressAggregate;

 public sealed class Address : AggregateRoot<AddressId, Guid>
{
    public string Province { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Street { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Address(
        AddressId addressId,
        string province,
        string city,
        string street,
        string postalCode)
        : base(addressId)
    {
        Province = province;
        City = city;
        Street = street;
        PostalCode = postalCode;
        UpdatedDateTime = DateTime.Now;
    }

    public static Address Create(
        string province,
        string city,
        string street,
        string postalCode)
    {
        // TODO: enforce invariants
        var user = new Address(
            AddressId.CreateUnique(),
            province,
            city,
            street,
            postalCode);

        return user;
    }

    public void Update(
        string province,
        string city,
        string street,
        string postalCode)
    {
        Province = province;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

#pragma warning disable CS8618
    private Address()
    {
    }
#pragma warning restore CS8618
}