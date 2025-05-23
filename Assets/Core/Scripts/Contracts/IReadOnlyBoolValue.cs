namespace Moyba.Contracts
{
    public interface IReadOnlyBoolValue : IReadOnlyValue<bool>
    {
        event SimpleEventHandler OnFalse;
        event SimpleEventHandler OnTrue;
    }

    public interface IReadOnlyBoolValue<TEntity> : IReadOnlyValue<TEntity, bool>
    {
        event SimpleEventHandler<TEntity> OnFalse;
        event SimpleEventHandler<TEntity> OnTrue;
    }
}
