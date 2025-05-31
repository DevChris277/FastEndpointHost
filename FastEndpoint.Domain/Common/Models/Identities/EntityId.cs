namespace FastEndpoint.Domain.Common.Models.Identities;

public abstract class EntityId<TId> : ValueObject
{
    protected EntityId(TId value)
    {
        Value = value;
    }

    protected EntityId()
    {
    }

    public TId Value { get; }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string? ToString()
    {
        return Value?.ToString() ?? base.ToString();
    }
}