namespace Moyba.Contracts
{
    public interface IReadOnlyBoolValue : IReadOnlyValue<bool>, IEventableBoolValue { }
    public interface IReadOnlyBoolValue<out TEntity> : IReadOnlyValue<TEntity, bool>, IEventableBoolValue<TEntity> { }
}
