using FastEndpoint.Domain.Common.Models.Identities;
using SequentialGuid;

namespace FastEndpoint.Domain.UserAggregate.ValueObject;

public sealed class FeUserId : AggregateRootId<Guid>
{
    private FeUserId(Guid value) : base(value)
    {
    }

    public static FeUserId CreateUnique()
    {
        return new FeUserId(SequentialGuidGenerator.Instance.NewGuid());
    }

    public static FeUserId Create(Guid userId)
    {
        return new FeUserId(userId);
    }
}