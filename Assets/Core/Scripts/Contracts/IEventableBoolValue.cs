namespace Moyba.Contracts
{
    public interface IEventableBoolValue : IEventableValue<bool>
    {
        event SimpleEventHandler OnFalse;
        event SimpleEventHandler OnTrue;
    }

    public interface IEventableBoolValue<out TEntity> : IEventableValue<TEntity, bool>
    {
        event SimpleEventHandler<TEntity> OnFalse;
        event SimpleEventHandler<TEntity> OnTrue;
    }
}
