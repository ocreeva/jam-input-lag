namespace Moyba.Contracts
{
    public interface IBoolValue : IValue<bool>, IReadOnlyBoolValue { }
    public interface IBoolValue<TEntity> : IValue<TEntity, bool>, IReadOnlyBoolValue<TEntity> { }
}
