using FastEndpoint.Domain.Common.Models.Identities;
using SequentialGuid;

namespace FastEndpoint.Domain.AddressAggregate.ValueObject;

public sealed class AddressId : AggregateRootId<Guid>
{
    private AddressId(Guid value) : base(value)
    {
    }

    public static AddressId CreateUnique()
    {
        return new AddressId(SequentialGuidGenerator.Instance.NewGuid());
    }

    public static AddressId Create(Guid addressId)
    {
        return new AddressId(addressId);
    }
}