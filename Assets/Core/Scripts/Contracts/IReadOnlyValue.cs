namespace Moyba.Contracts
{
    public interface IReadOnlyValue<TValue>
    {
        event ValueEventHandler<TValue> OnChanged;

        TValue Value { get; }
    }

    public interface IReadOnlyValue<TEntity, TValue>
    {
        event ValueEventHandler<TEntity, TValue> OnChanged;

        TValue Value { get; }
    }

}
