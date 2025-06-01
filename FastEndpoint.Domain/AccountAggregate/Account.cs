using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using FastEndpoint.Domain.Common.Models;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;

namespace FastEndpoint.Domain.AccountAggregate;

public sealed class Account : AggregateRoot<AccountId, Guid>
{
    private readonly List<CustomerId> _customerIds = new();
    public string Name { get; private set; } = null!;
    public string MobileNumber { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public AddressId AddressId { get; private set; } = null!;

    public IReadOnlyList<CustomerId> CustomerIds => _customerIds.AsReadOnly();

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Account(
        AccountId accountId,
        string name,
        string mobileNumber,
        string email,
        AddressId addressId)
        : base(accountId)
    {
        Name = name;
        MobileNumber = mobileNumber;
        Email = email;
        AddressId = addressId;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static Account Create(
        string name,
        string mobileNumber,
        string email,
        AddressId addressId)
    {
        // TODO: enforce invariants
        var account = new Account(
            AccountId.CreateUnique(),
            name,
            mobileNumber,
            email,
            addressId);

        return account;
    }

    public void AddCustomerId(CustomerId customerId)
    {
        _customerIds.Add(customerId);
    }

    public void Update(
        string name,
        string mobileNumber,
        string email,
        AddressId addressId)
    {
        Name = name;
        MobileNumber = mobileNumber;
        Email = email;
        AddressId = addressId;
        UpdatedDateTime = DateTime.UtcNow;
    }

#pragma warning disable CS8618
    private Account()
    {
    }
#pragma warning restore CS8618
}