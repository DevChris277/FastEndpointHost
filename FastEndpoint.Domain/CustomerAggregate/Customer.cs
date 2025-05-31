using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using FastEndpoint.Domain.Common.Models;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;

namespace FastEndpoint.Domain.CustomerAggregate;

public sealed class Customer : AggregateRoot<CustomerId, Guid>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string MobileNumber { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public AccountId AccountId { get; private set; } = null!;
    public AddressId AddressId { get; private set; } = null!;

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Customer(
        CustomerId customerId,
        string firstName,
        string lastName,
        string mobileNumber,
        string email,
        AccountId accountId,
        AddressId addressId)
        : base(customerId)
    {
        FirstName = firstName;
        LastName = lastName;
        MobileNumber = mobileNumber;
        Email = email;
        AccountId = accountId;
        AddressId = addressId;
    }

    public static Customer Create(
        string firstName,
        string lastName,
        string mobileNumber,
        string email,
        AccountId accountId,
        AddressId addressId)
    {
        // TODO: enforce invariants
        var customer = new Customer(
            CustomerId.CreateUnique(),
            firstName,
            lastName,
            mobileNumber,
            email,
            accountId,
            addressId);

        // customer.AddDomainEvent(new CustomerCreated(customer));

        return customer;
    }

    public void Update(
        string firstName,
        string lastName,
        string mobileNumber,
        string email,
        AccountId accountId,
        AddressId addressId)
    {
        FirstName = firstName;
        LastName = lastName;
        MobileNumber = mobileNumber;
        Email = email;
        AccountId = accountId;
        AddressId = addressId;
    }

#pragma warning disable CS8618
    private Customer()
    {
    }
#pragma warning restore CS8618
}