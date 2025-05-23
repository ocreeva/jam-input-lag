namespace Moyba.Contracts
{
    internal static class _ContractExtensions
    {
        public static void OnBoolean(
            this IBoolValue value,
            SimpleEventHandler onFalse,
            SimpleEventHandler onTrue)
        => value.OnChanged += (bool boolean) => (boolean ? onTrue : onFalse)?.Invoke();

        public static void OnBoolean<TEntity>(
            this IBoolValue<TEntity> value,
            SimpleEventHandler<TEntity> onFalse,
            SimpleEventHandler<TEntity> onTrue)
        => value.OnChanged += (TEntity entity, bool boolean) => (boolean ? onTrue : onFalse)?.Invoke(entity);
    }
}
