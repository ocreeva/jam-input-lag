using Moyba.Contracts;

namespace Moyba.SFX
{
    public interface ISFXManager
    {
        IMuteable Music { get; }
        IMuteable Sound { get; }
        IValue<float> Volume { get; }
    }
}
