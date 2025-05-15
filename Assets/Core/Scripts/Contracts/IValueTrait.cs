namespace Moyba.Contracts
{
    public interface IValueTrait<T>
    {
        event ValueEventHandler<T> OnValueChanged;
        event ValueEventHandler<T> OnValueChanging;

        T Value { get; set; }
    }
}
