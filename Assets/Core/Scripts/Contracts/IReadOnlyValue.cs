namespace Moyba.Contracts
{
    public interface IReadOnlyValue<T>
    {
        event ValueEventHandler<T> OnChanged;
        event ValueEventHandler<T> OnChanging;

        T Value { get; }
    }
}
