namespace Moyba.Contracts
{
    public interface IReadOnlyValue<out TValue> : IEventableValue<TValue>
    {
        TValue Value { get; }
    }

    public interface IReadOnlyValue<out TEntity, out TValue> : IEventableValue<TEntity, TValue>
    {
        TValue Value { get; }
    }

}
