namespace Moyba.Contracts
{
    public interface IValue<T>
    {
        event ValueEventHandler<T> OnChanged;
        event ValueEventHandler<T> OnChanging;

        T Value { get; set; }
    }
}
