namespace Moyba.Contracts
{
    public interface IBoolValue : IValue<bool>
    {
        event SimpleEventHandler OnFalse;
        event SimpleEventHandler OnTrue;
    }
}
