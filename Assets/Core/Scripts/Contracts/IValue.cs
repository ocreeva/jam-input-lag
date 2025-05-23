namespace Moyba.Contracts
{
    public interface IValue<TValue> : IReadOnlyValue<TValue>
    {
        new TValue Value { get; set; }
    }
}
