namespace Moyba.Contracts
{
    public interface IReadOnlyBoolValue : IReadOnlyValue<bool>
    {
        event SimpleEventHandler OnFalse;
        event SimpleEventHandler OnTrue;
    }
}
