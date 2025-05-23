namespace Moyba.Contracts
{
    public interface IValue<TValue> : IEventableValue<TValue>, IReadOnlyValue<TValue>
    {
        new TValue Value { get; set; }
    }

    public interface IValue<out TEntity, TValue> : IEventableValue<TEntity, TValue>, IReadOnlyValue<TEntity, TValue>
    {
        new TValue Value { get; set; }
    }
}
