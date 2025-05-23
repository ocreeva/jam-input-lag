namespace Moyba.Contracts
{
    internal static class _ContractExtensions
    {
        public static void OnBoolean(this IBoolValue value, SimpleEventHandler onFalse, SimpleEventHandler onTrue)
        => value.OnChanged += (bool boolean) => (boolean ? onTrue : onFalse)();
    }
}
