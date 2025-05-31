using FastEndpoint.Domain.Common.Models.Identities;
using SequentialGuid;

namespace FastEndpoint.Domain.JobCardAggregate.ValueObjects;

public sealed class JobCardId : AggregateRootId<Guid>
{
    private JobCardId(Guid value) : base(value)
    {
    }

    public static JobCardId CreateUnique()
    {
        return new JobCardId(SequentialGuidGenerator.Instance.NewGuid());
    }

    public static JobCardId Create(Guid jobcardId)
    {
        return new JobCardId(jobcardId);
    }
}