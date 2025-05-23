namespace Moyba.Contracts
{
    public interface IBoolValue : IValue<bool>, IReadOnlyBoolValue { }
    public interface IBoolValue<out TEntity> : IValue<TEntity, bool>, IReadOnlyBoolValue<TEntity> { }
}
