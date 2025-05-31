using FastEndpoint.Domain.Common.Models.Identities;
using SequentialGuid;

namespace FastEndpoint.Domain.CustomerAggregate.ValueObjects;

public sealed class CustomerId : AggregateRootId<Guid>
{
    private CustomerId(Guid value) : base(value)
    {
    }

    public static CustomerId CreateUnique()
    {
        return new CustomerId(SequentialGuidGenerator.Instance.NewGuid());
    }

    public static CustomerId Create(Guid customerId)
    {
        return new CustomerId(customerId);
    }
}