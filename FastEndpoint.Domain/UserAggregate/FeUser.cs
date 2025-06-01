using FastEndpoint.Domain.Common.Models;
using FastEndpoint.Domain.UserAggregate.ValueObject;

namespace FastEndpoint.Domain.UserAggregate;

public class FeUser :AggregateRoot<FeUserId, Guid>
{
    private FeUser(
        FeUserId userId,
        string firstName,
        string lastName,
        string role,
        string email,
        string password)
        : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        Password = password;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

#pragma warning disable CS8618
    private FeUser()
    {
    }
#pragma warning restore CS8618
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Role { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!; // TODO: Hash this

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static FeUser Create(
        string firstName,
        string lastName,
        string role,
        string email,
        string password)
    {
        // TODO: enforce invariants
        var user = new FeUser(
            FeUserId.CreateUnique(),
            firstName,
            lastName,
            role,
            email,
            password);

        return user;
    }
}