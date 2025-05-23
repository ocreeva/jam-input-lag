namespace Moyba.Contracts
{
    public interface IReadOnlyValue<TValue>
    {
        event ValueEventHandler<TValue> OnChanged;

        TValue Value { get; }
    }
}
