using FastEndpoint.Domain.Common.Models.Identities;
using SequentialGuid;

namespace FastEndpoint.Domain.AccountAggregate.ValueObjects;

public sealed class AccountId : AggregateRootId<Guid>
{
    private AccountId(Guid value) : base(value)
    {
    }

    public static AccountId CreateUnique()
    {
        return new AccountId(SequentialGuidGenerator.Instance.NewGuid());
    }

    public static AccountId Create(Guid accountId)
    {
        return new AccountId(accountId);
    }
}