namespace Moyba.Contracts
{
    public interface IValue<TValue> : IReadOnlyValue<TValue>
    {
        new TValue Value { get; set; }
    }

    public interface IValue<TEntity, TValue> : IReadOnlyValue<TEntity, TValue>
    {
        new TValue Value { get; set; }
    }
}
