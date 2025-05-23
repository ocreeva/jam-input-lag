namespace Moyba.Contracts
{
    public interface IEventableValue<out TValue>
    {
        event ValueEventHandler<TValue> OnChanged;
    }

    public interface IEventableValue<out TEntity, out TValue>
    {
        event ValueEventHandler<TEntity, TValue> OnChanged;
    }
}
